import { UserManager, UserManagerSettings, WebStorageStateStore, Log } from "oidc-client";
import { ApplicationPaths, METADATA_OIDC, APPLICATION_NAME } from "./AuthConstants";


export default
    class AuthService {

    userManager: UserManager;

    constructor() {
        Log.level = Log.DEBUG;
        Log.logger = console;
    }

    async ensureUserManagerInitialized() {
        if (this.userManager !== undefined) {
            return;
        }

        let response = await fetch(ApplicationPaths.ApiAuthorizationClientConfigurationUrl);
        if (!response.ok) {
            throw new Error(`Could not load settings for '${APPLICATION_NAME}'`);
        }

        let settings: UserManagerSettings = await response.json();
        this.userManager = new UserManager({
            ...settings,
            metadata: METADATA_OIDC(settings.authority),
            automaticSilentRenew: true,
            includeIdTokenInSilentRenew: true,
            userStore: new WebStorageStateStore({
                prefix: APPLICATION_NAME
            })
        });

        this.userManager.events.addUserSignedOut(async () => {
            await this.userManager?.removeUser();
            this.updateState(undefined);
        });
        this.userManager.events.addUserLoaded((user) => {
            if (window.location.href.indexOf("signin-oidc") !== -1) {
                this.navigateToScreen();
            }
        });
        this.userManager.events.addSilentRenewError((e) => {
            console.log("silent renew error", e.message);
        });

        this.userManager.events.addAccessTokenExpired(() => {
            console.log("token expired");
            this.signinSilent();
        });
    }
    signinSilent() {
        throw new Error("Method not implemented.");
    }
    navigateToScreen() {
        throw new Error("Method not implemented.");
    }

    updateState(undefined: undefined) {
        throw new Error("Method not implemented.");
    }
}