using System;
using AVFoundation;
using AVKit;
using CoreGraphics;
using CoreMedia;
using Foundation;
using MediaAccessibility;
using ObjCRuntime;
using UIKit;
using Google.Cast;
using OoyalaSDK.iOS;

namespace OoyalaCastSDK.iOS
{
    // @interface OOCastButton : UIButton
    [BaseType(typeof(UIButton))]
    [Protocol]
    interface OOCastButton
    {
        // -(void)startCastButtonAnimating;
        [Export("startCastButtonAnimating")]
        void StartCastButtonAnimating();

        // -(void)updateCastButtonFrameColor:(BOOL)isConnectedToChromecast;
        [Export("updateCastButtonFrameColor:")]
        void UpdateCastButtonFrameColor(bool isConnectedToChromecast);
    }

    // @protocol OOCastManagerDelegate
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface OOCastManagerDelegate
    {
        // @required -(UIViewController *)currentTopUIViewController;
        [Abstract]
        [Export("currentTopUIViewController")]
        UIViewController CurrentTopUIViewController { get; }
    }

    [Static]
    //[Verify(ConstantsInterfaceAssociation)]
    partial interface Constants
    {
        // extern NSString *const OOCastManagerDidDisconnectNotification;
        [Field("OOCastManagerDidDisconnectNotification", "__Internal")]
        NSString OOCastManagerDidDisconnectNotification { get; }

        // extern NSString *const OOCastEnterCastModeNotification;
        [Field("OOCastEnterCastModeNotification", "__Internal")]
        NSString OOCastEnterCastModeNotification { get; }

        // extern NSString *const OOCastExitCastModeNotification;
        [Field("OOCastExitCastModeNotification", "__Internal")]
        NSString OOCastExitCastModeNotification { get; }

        // extern NSString *const OOCastMiniControllerClickedNotification;
        [Field("OOCastMiniControllerClickedNotification", "__Internal")]
        NSString OOCastMiniControllerClickedNotification { get; }

        // extern NSString *const OOCastErrorNotification;
        [Field("OOCastErrorNotification", "__Internal")]
        NSString OOCastErrorNotification { get; }
    }

    // @interface OOCastManager : UIViewController
    [BaseType(typeof(UIViewController))]
    interface OOCastManager : IOOCastManagerProtocol
    {
        // @property (readonly, nonatomic) GCKDevice * selectedDevice;
        [Export("selectedDevice")]
        Device SelectedDevice { get; }

        //[Wrap("WeakDelegate")]
        //IOOCastManagerDelegate Delegate { get; set; }

        // @property (nonatomic, weak) id<OOCastManagerDelegate> delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Weak)]
        NSObject WeakDelegate { get; set; }

        // +(OOCastManager *)castManagerWithAppID:(NSString *)receiverAppID namespace:(NSString *)appNamespace;
        [Static]
        [Export("castManagerWithAppID:namespace:")]
        OOCastManager CastManagerWithAppID(string receiverAppID, string appNamespace);

        // -(void)disconnectFromOoyalaPlayer;
        [Export("disconnectFromOoyalaPlayer")]
        void DisconnectFromOoyalaPlayer();

        // -(UIButton *)castButton;
        [Export("castButton")]
        //[Verify(MethodToProperty)]
        UIButton CastButton { get; }

        // -(void)setCastModeVideoView:(UIView *)castView;
        [Export("setCastModeVideoView:")]
        void SetCastModeVideoView(UIView castView);

        // -(void)setAdditionalInitParams:(NSDictionary *)params;
        [Export("setAdditionalInitParams:")]
        void SetAdditionalInitParams(NSDictionary @params);
    }

    // @interface OOCastManagerDeviceReconnector : NSObject
    [BaseType(typeof(NSObject))]
    [Protocol]
    [DisableDefaultCtor]
    interface OOCastManagerDeviceReconnector
    {
        // @property (nonatomic) BOOL automaticallyReconnect;
        [Export("automaticallyReconnect")]
        bool AutomaticallyReconnect { get; set; }

        // @property (readonly, nonatomic) BOOL isReconnecting;
        [Export("isReconnecting")]
        bool IsReconnecting { get; }

        // @property (readonly, nonatomic) NSString * lastSessionID;
        [Export("lastSessionID")]
        string LastSessionID { get; }

        // -(instancetype)initWithCastManager:(OOCastManager *)castManager;
        [Export("initWithCastManager:")]
        IntPtr Constructor(OOCastManager castManager);

        // -(void)rememberDevice:(GCKDevice *)device sessionID:(NSString *)sessionID;
        [Export("rememberDevice:sessionID:")]
        void RememberDevice(Device device, string sessionID);

        // -(void)deviceDidComeOnline:(GCKDevice *)device;
        [Export("deviceDidComeOnline:")]
        void DeviceDidComeOnline(Device device);

        // -(void)didFailToConnectWithError:(NSError *)error;
        [Export("didFailToConnectWithError:")]
        void DidFailToConnectWithError(NSError error);

        // -(void)didFailToConnectToApplicationWithError:(id)error;
        [Export("didFailToConnectToApplicationWithError:")]
        void DidFailToConnectToApplicationWithError(NSObject error);

        // -(BOOL)didDisconnectWithError:(id)error;
        [Export("didDisconnectWithError:")]
        bool DidDisconnectWithError(NSObject error);

        // -(void)forgetReconnectionInfo;
        [Export("forgetReconnectionInfo")]
        void ForgetReconnectionInfo();
    }

    // @protocol OOCastMiniControllerProtocol <NSObject>
    [Protocol]
    [BaseType(typeof(NSObject))]
    interface OOCastMiniControllerProtocol
    {
        // @required -(void)updatePlayState:(BOOL)isPlaying;
        [Abstract]
        [Export("updatePlayState:")]
        void UpdatePlayState(bool isPlaying);

        // @required -(void)dismiss;
        [Abstract]
        [Export("dismiss")]
        void Dismiss();
    }

    // @protocol OOCastMiniControllerDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface OOCastMiniControllerDelegate
    {
        // @required -(void)onDismissMiniController:(id<OOCastMiniControllerProtocol>)miniControllerView;
        [Abstract]
        [Export("onDismissMiniController:")]
        void OnDismissMiniController(OOCastMiniControllerProtocol miniControllerView);
    }

    // @interface OOCastMenuViewController : UITableViewController <OOCastMiniControllerDelegate>
    [BaseType(typeof(UITableViewController))]
    [Protocol]
    interface OOCastMenuViewController : OOCastMiniControllerDelegate
    {
        // -(id)initWithCastManager:(OOCastManager *)castManager;
        [Export("initWithCastManager:")]
        IntPtr Constructor(OOCastManager castManager);
    }

    // @interface OOCastMiniControllerView : UIView <OOCastMiniControllerProtocol>
    [BaseType(typeof(UIView))]
    interface OOCastMiniControllerView : OOCastMiniControllerProtocol
    {
        // @property (nonatomic, strong) UITableViewCell * cell;
        [Export("cell", ArgumentSemantic.Strong)]
        UITableViewCell Cell { get; set; }

        // -(instancetype)initWithFrame:(CGRect)frame castManager:(OOCastManager *)castManager delegate:(id<OOCastMiniControllerDelegate>)delegate;
        [Export("initWithFrame:castManager:delegate:")]
        IntPtr Constructor(CGRect frame, OOCastManager castManager, NSObject @delegate);
    }

    // @interface OOCastPlayer
    //[Protocol]
    [DisableDefaultCtor]
    [BaseType(typeof(CastChannel))]
    interface OOCastPlayer : IOOPlayerProtocol
    {
        // @property (nonatomic) int * stateNotifier;
        //[Export("stateNotifier", ArgumentSemantic.Assign)]
        //unsafe int* StateNotifier { get; set; }

        [Export("stateNotifier", ArgumentSemantic.Assign)]
        OOStateNotifier StateNotifier { get; set; }

        // @property (nonatomic) Float64 playheadTime;
        [Export("playheadTime")]
        double PlayheadTime { get; set; }

        //[Abstract]
        [Export("state")]
        OOOoyalaPlayerState State { get; }

        // @property (nonatomic) NSString * embedCode;
        [Export("embedCode")]
        string EmbedCode { get; set; }

        // @property (nonatomic) Float64 playheadTime;
        //[Export("playheadTime")]
        //double PlayheadTime { get; set; }

        // @property (nonatomic) NSString * castItemTitle;
        [Export("castItemTitle")]
        string CastItemTitle { get; set; }

        // @property (nonatomic) NSString * castItemDescription;
        [Export("castItemDescription")]
        string CastItemDescription { get; set; }

        // @property (nonatomic) NSString * castItemPromoImg;
        [Export("castItemPromoImg")]
        string CastItemPromoImg { get; set; }

        // @property (readonly, nonatomic) BOOL isMiniControllerInteractionAvailable;
        [Export("isMiniControllerInteractionAvailable")]
        bool IsMiniControllerInteractionAvailable { get; }

        // -(instancetype)initWithNamespace:(NSString *)appNamespace castSession:(id)castSession castManager:(OOCastManager *)castManager;
        [Export("initWithNamespace:castSession:castManager:")]
        IntPtr Constructor(string appNamespace, NSObject castSession, OOCastManager castManager);

        // -(void)initStateNotifier:(id)stateNotifier;
        [Export("initStateNotifier:")]
        void InitStateNotifier(NSObject stateNotifier);

        // -(void)registerWithOoyalaPlayer:(OOOoyalaPlayer *)ooyalaPlayer;
        [Export("registerWithOoyalaPlayer:")]
        void RegisterWithOoyalaPlayer(OOOoyalaPlayer ooyalaPlayer);

        // -(void)updateMetadataFromOoyalaPlayer:(NSString *)castItemPromoImg castItemTitle:(NSString *)castItemTitle castItemDescription:(NSString *)castItemDescription;
        [Export("updateMetadataFromOoyalaPlayer:castItemTitle:castItemDescription:")]
        void UpdateMetadataFromOoyalaPlayer(string castItemPromoImg, string castItemTitle, string castItemDescription);

        // -(void)enterCastModeWithOptions:(OOCastModeOptions *)options embedToken:(NSString *)embedToken additionalInitParams:(NSDictionary *)params;
        [Export("enterCastModeWithOptions:embedToken:additionalInitParams:")]
        void EnterCastModeWithOptions(OOCastModeOptions options, string embedToken, NSDictionary @params);

        // -(void)registerMiniController:(id<OOCastMiniControllerProtocol>)miniController;
        [Export("registerMiniController:")]
        // TODO: Verify
        void RegisterMiniController(OOCastMiniControllerView miniController);

        // -(void)deregisterMiniController:(id<OOCastMiniControllerProtocol>)miniController;
        [Export("deregisterMiniController:")]
        void DeregisterMiniController(OOCastMiniControllerView miniController);

        // -(void)onExitCastMode;
        [Export("onExitCastMode")]
        void OnExitCastMode();

        // -(void)forceAssetRejoin;
        [Export("forceAssetRejoin")]
        void ForceAssetRejoin();
    }

    // @interface OOCastUtils : NSObject
    [BaseType(typeof(NSObject))]
    interface OOCastUtils
    {
        // +(void)postCastErrorNotificationFrom:(id)from error:(NSError *)error extras:(NSDictionary *)extras;
        [Static]
        [Export("postCastErrorNotificationFrom:error:extras:")]
        void PostCastErrorNotificationFrom(NSObject from, NSError error, NSDictionary extras);
    }
}
