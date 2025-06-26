namespace BedrockProtocol.Types;

public enum PlayStatus {
    
    LoginSuccess = 0,
    LoginFailedClientOld = 1,
    LoginFailedServerOld = 2,
    PlayerSpawn = 3,
    LoginFailedInvalidTenant = 4,
    LoginFailedMismatchEduToVanilla = 5,
    LoginFailedMismatchVanillaToEdu = 6,
    LoginFailedServerFull = 7,
    LoginFailedMismatchEditorToVanilla = 8,
    LoginFailedMismatchVanillaToEditor = 9,
}