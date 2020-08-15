using System.Collections;
using System.Collections.Generic;
using UnityEngine.Advertisements;
using UnityEngine;

public class UnityAdsScript : MonoBehaviour, IUnityAdsListener 
{
    public static UnityAdsScript instance;

    string gameId = "3769445";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;

    private void Awake() {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        Advertisement.AddListener (this);
        Advertisement.Initialize (gameId, testMode);
    }

    private void MakeInstance() {
        if(instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ShowInterstitialAd() {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady()) {
            Advertisement.Show();
        } 
        else {
            Debug.Log("Interstitial ad not ready at the moment! Please try again later!");
        }
    }

    public void ShowRewardedVideo() {
        // Check if UnityAds ready before calling Show method:
        if (Advertisement.IsReady(myPlacementId)) {
            Advertisement.Show(myPlacementId);
        } 
        else {
            Debug.Log("Rewarded video is not ready at the moment! Please try again later!");
        }
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
            // Reward the user for watching the ad to completion.
            Advertisement.RemoveListener(this);
            Advertisement.AddListener (this);
            Debug.Log("You finished the full Ad");
        } else if (showResult == ShowResult.Skipped) {
            // Do not reward the user for skipping the ad.
            Advertisement.RemoveListener(this);
            Advertisement.AddListener (this);
            Debug.Log("You didn't finish the full Ad");
        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error.");
            Advertisement.RemoveListener(this);
            Advertisement.AddListener (this);
        }
    }

    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId) {
            // Optional actions to take when the placement becomes ready(For example, enable the rewarded ads button)
        }
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    } 

    // When the object that subscribes to ad events is destroyed, remove the listener:
    public void OnDestroy() {
        
    }
}
