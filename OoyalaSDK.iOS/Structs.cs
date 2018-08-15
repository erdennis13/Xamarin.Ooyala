using System;
using System.Runtime.InteropServices;
using ObjCRuntime;

namespace OoyalaSDK.iOS
{
	[Native]
    public enum OOClosedCaptionPresentation : long
    {
        PopOn,
        RollUp,
        PaintOn
    }

    [Native]
    public enum OOOoyalaPlayerVideoGravity : long
    {
        OOOoyalaPlayerVideoGravityResize,
        Aspect,
        AspectFill
    }

    [Native]
    public enum OOOoyalaPlayerState : ulong
    {
        Init,
        Loading,
        Ready,
        Playing,
        Paused,
        Completed,
        Error
    }

    [Native]
    public enum OOOoyalaPlayerDesiredState : long
    {
        laying,
        aused
    }

    [Native]
    public enum OOAdMode : long
    {
        None,
        ContentChanged,
        InitialPlay,
        Playhead,
        CuePoint,
        ContentFinished,
        ContentError,
        PluginInitiated
    }

    //[StructLayout (LayoutKind.Sequential)]
    //public struct OOTBXMLAttribute
    //{
    //    public unsafe sbyte* name;

    //    public unsafe sbyte* value;

    //    public unsafe _OOTBXMLAttribute* next;
    //}

    //[StructLayout (LayoutKind.Sequential)]
    //public struct OOTBXMLElement
    //{
    //    public unsafe sbyte* name;

    //    public unsafe sbyte* text;

    //    public unsafe OOTBXMLAttribute* firstAttribute;

    //    public unsafe _OOTBXMLElement* parentElement;

    //    public unsafe OOTBXMLElement* firstChild;

    //    public unsafe _OOTBXMLElement* currentChild;

    //    public unsafe _OOTBXMLElement* nextSibling;

    //    public unsafe _OOTBXMLElement* previousSibling;
    //}

    //[StructLayout (LayoutKind.Sequential)]
    //public struct OOTBXMLElementBuffer
    //{
    //    public unsafe OOTBXMLElement* elements;

    //    public unsafe _OOTBXMLElementBuffer* next;

    //    public unsafe _OOTBXMLElementBuffer* previous;
    //}

    //[StructLayout (LayoutKind.Sequential)]
    //public struct OOTBXMLAttributeBuffer
    //{
    //    public unsafe OOTBXMLAttribute* attributes;

    //    public unsafe _OOTBXMLAttributeBuffer* next;

    //    public unsafe _OOTBXMLAttributeBuffer* previous;
    //}

    [Native]
    public enum OOReturnState : long
    {
        Matched,
        Unmatched,
        Fail
    }

    [Native]
    public enum OOAuthCode : long
    {
        Unknown = -2,
        NotRequested = -1,
        Min = 0,
        Authorized = 0,
        UnauthorizedParent = 1,
        UnauthorizedDomain = 2,
        UnauthorizedLocation = 3,
        BeforeFlightTime = 4,
        AfterFlightTime = 5,
        OutsideRecurringFlightTimes = 6,
        BadEmbedCode = 7,
        InvalidSignature = 8,
        MissingParams = 9,
        MissingRuleSet = 10,
        Unauthorized = 11,
        MissingPcode = 12,
        UnauthorizedDevice = 13,
        InvalidToken = 14,
        TokenExpired = 15,
        UnauthorizedMultiSyndGroup = 16,
        ProviderDeleted = 17,
        TooManyActiveStreams = 18,
        MissingAccountId = 19,
        NoEntitlementsFound = 20,
        NonEntitledDevice = 21,
        NonRegisteredDevice = 22,
        ProviderOverCapTrial = 23,
        ProxyDetected = 24,
        Max = 24
    }

    [Native]
    public enum OOOoyalaPlayerActionAtEnd : long
    {
        Continue,
        Pause,
        Stop,
        Reset
    }

    [Native]
    public enum OOOoyalaPlayerEnvironment : long
    {
        Production,
        Staging,
        NextStaging,
        Local
    }

    [Native]
    public enum OOSeekStyle : long
    {
        None,
        Basic,
        Enhanced
    }

    [Native]
    public enum OOUIProgressSliderMode : long
    {
        Live,
        AdInLive,
        Normal,
        LiveNoSrubber,
        ElapsedDuration
    }

    [Native]
    public enum OOFCCTvRatingsPosition : long
    {
        TopLeft,
        TopRight,
        BottomLeft,
        BottomRight
    }

    [Native]
    public enum RequiredType : long
    {
        All,
        Any,
        None
    }

    [Native]
    public enum OffsetType : long
    {
        Seconds,
        Percentage,
        Position
    }

    [Native]
    public enum OOType : long
    {
        None,
        Static,
        IFrame,
        Html
    }

    [Native]
    public enum ResourceType : long
    {
        Static,
        IFrame,
        Html
    }

    [Native]
    public enum OOOoyalaPlayerControlType : long
    {
        Inline,
        FullScreen
    }

    [Native]
    public enum OOOoyalaErrorCode : long
    {
        AuthorizationFailed = 0,
        AuthorizationInvalid = 1,
        HeartbeatFailed = 2,
        ContentTreeInvalid = 3,
        AuthorizationSignatureInvalid = 4,
        ContentTreeNextFailed = 5,
        PlaybackFailed = 6,
        AssetNotEncodedForIOS = 7,
        InternalIOS = 8,
        MetadataInvalid = 9,
        DeviceInvalidAuthToken = 10,
        DeviceLimitReached = 11,
        DeviceBindingFailed = 12,
        DeviceIdTooLong = 13,
        DeviceGenericDrmError = 14,
        DrmDownloadFailedError = 15,
        DrmPersonalizationFailedError = 16,
        DrmAcquireRightsFailedError = 17,
        DiscoveryInvalidParameter = 18,
        DiscoveryNetworkError = 19,
        DiscoveryFailedResponse = 20,
        NoAvailableStreams = 21,
        PcodeMismatch = 22,
        DownloadFailed = 23,
        DeviceConcurrentStreams = 24,
        AdvertistingIdFailure = 25,
        DiscoveryGetFailure = 26,
        DiscoveryPostFailure = 27,
        PlayerFormatMissmatch = 28,
        CreateVRPlayerFailed = 29,
        UnknownError = 30,
        GeoBlockingError = 31
    }

    [Native]
    public enum OODiscoveryType : ulong
    {
        Momentum = 0,
        Popular,
        SimilarAssets
    }

    [Native]
    public enum DebugMode : long
    {
        None,
        LogOnly,
        LogAndAbort
    }
}
