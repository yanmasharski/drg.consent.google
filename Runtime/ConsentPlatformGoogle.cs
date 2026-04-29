namespace DRG.Consent
{
    using System;
    using System.Threading.Tasks;
    using GoogleMobileAds.Ump.Api;
    using DRG.Logs;

    public class ConsentPlatformGoogle : IConsentPlatform
    {
        private readonly ConsentRequestParameters request;
        private readonly ILogger logger;
        private bool isInitialized = false;
        private Action delayedAction = null;

        public ConsentPlatformGoogle(ILogger logger)
        {
            this.logger = logger;
            request = new ConsentRequestParameters();
            ConsentInformation.Update(request, OnConsentInfoUpdated);
        }

        public ConsentState state { get; private set; }

        public bool TryShowConsentDialog(Action<bool> completed)
        {
            if ((isInitialized &&
                ConsentInformation.PrivacyOptionsRequirementStatus != PrivacyOptionsRequirementStatus.Required) ||
                state == ConsentState.Accepted)
            {
                completed?.Invoke(true);
                return false;
            }

            if (!isInitialized)
            {
                delayedAction = () => TryShowConsentDialog(completed);
                return true;
            }

            ConsentForm.LoadAndShowConsentFormIfRequired((FormError formError) =>
            {
                if (formError != null)
                {
                    logger.LogError(formError.ToString());
                    completed?.Invoke(false);
                    return;
                }

                logger.Log($"Consent form shown");

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
                logger.LogError(consentError.ToString());
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

            logger.Log($"Consent state updated: {ConsentInformation.PrivacyOptionsRequirementStatus}{state}{canRequestAds}");

            isInitialized = true;
            delayedAction?.Invoke();
            delayedAction = null;
        }
    }
}
