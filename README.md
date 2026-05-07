# DRG Consent — Google UMP

Google UMP (User Messaging Platform) adapter for `drg.consent`.

## Assembly

`DRG.Consent.Google` — `ConsentPlatformGoogle`

## Key types

- **`ConsentPlatformGoogle`** — `IConsentPlatform` implementation using the Google UMP SDK. Requests consent information, shows the form if required, and maps the result to `ConsentState`.

## Usage

```csharp
var consent = new ConsentPlatformGoogle(logger);
locator.Register<IConsentPlatform>(consent);
```

## External requirement

Google Mobile Ads Unity plugin must be installed separately. UMP is bundled with GMA.  
Install via OpenUPM: `https://developers.google.com/admob/unity/quick-start#openupm`

## Dependencies

- `com.drg.consent`
- `com.drg.core`

## Install

```
https://github.com/yanmasharski/drg.consent.google.git#0.9.0
```
