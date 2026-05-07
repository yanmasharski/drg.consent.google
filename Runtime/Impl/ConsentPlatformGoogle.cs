using System;
using System.Threading.Tasks;
using DRG.Core.Logs;
using GoogleMobileAds.Ump.Api;

namespace DRG.Consent
{
	public class ConsentPlatformGoogle : IConsentPlatform
	{
		private readonly ConsentRequestParameters _request;
		private readonly ILogger _logger;
		private bool _isInitialized = false;
		private Action _delayedAction = null;

		public ConsentPlatformGoogle(ILogger logger)
		{
			_logger = logger;
			_request = new ConsentRequestParameters();
			ConsentInformation.Update(_request, OnConsentInfoUpdated);
		}

		public ConsentState state { get; private set; }

		public bool TryShowConsentDialog(Action<bool> completed)
		{
			if ((_isInitialized &&
				ConsentInformation.PrivacyOptionsRequirementStatus != PrivacyOptionsRequirementStatus.Required) ||
				state == ConsentState.Accepted)
			{
				completed?.Invoke(true);
				return false;
			}

			if (!_isInitialized)
			{
				_delayedAction = () => TryShowConsentDialog(completed);
				return true;
			}

			ConsentForm.LoadAndShowConsentFormIfRequired((FormError formError) =>
			{
				if (formError != null)
				{
					_logger.LogError(formError.ToString());
					completed?.Invoke(false);
					return;
				}

				_logger.Log($"Consent form shown");

#if UNITY_EDITOR
				completed?.Invoke(false);
#else
                completed?.Invoke(true);
#endif
			});

			return true;
		}

		public async Task<bool> TryShowConsentDialogAsync()
		{
			var tcs = new TaskCompletionSource<bool>();
			TryShowConsentDialog(result => tcs.SetResult(result));
			return await tcs.Task;
		}

		private void OnConsentInfoUpdated(FormError consentError)
		{
			if (consentError != null)
			{
				_logger.LogError(consentError.ToString());
				return;
			}

			var canRequestAds = ConsentInformation.CanRequestAds();
#if UNITY_EDITOR
			canRequestAds = false;
#endif

			state = ConsentInformation.PrivacyOptionsRequirementStatus !=
				PrivacyOptionsRequirementStatus.Required
					? ConsentState.NotApplicable
					: (canRequestAds ? ConsentState.Accepted : ConsentState.Unknown);

			_logger.Log($"Consent state updated: {ConsentInformation.PrivacyOptionsRequirementStatus}{state}{canRequestAds}");

			_isInitialized = true;
			_delayedAction?.Invoke();
			_delayedAction = null;
		}
	}
}
