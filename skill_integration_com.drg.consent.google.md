# Integration: `com.drg.consent.google`

**For LLMs:** step-by-step integration notes for Unity package `com.drg.consent.google`—dependencies, project setup, minimal usage, rollout checklist. (Placeholder.)

## Google Unity packages (Mobile Ads / UMP)

**Unity package ID (host project):**

- **`com.google.ads.mobile`** — Google Mobile Ads Unity plugin; includes **UMP** (`GoogleMobileAds.Ump.Api`) required by `ConsentPlatformGoogle`.

**Distribution:** AdMob / Mobile Ads is **not** on [Google APIs for Unity — archive](https://developers.google.com/unity/archive). Install per [AdMob Unity quick start](https://developers.google.com/admob/unity/quick-start) (OpenUPM scoped registry for `com.google`, GitHub `.unitypackage`, etc.).

**Recommendation:** For Google Unity packages that *do* appear on the archive (Firebase, Play libraries, EDM, …), prefer `.tgz` URLs from [Google APIs for Unity — archive](https://developers.google.com/unity/archive) in `manifest.json`.

## Status

Stub. Expand when ready.
