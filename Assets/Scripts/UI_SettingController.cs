using MoreMountains.NiceVibrations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI_SettingController : MonoBehaviour
{
    public static UI_SettingController Instance;
    public SoundHapticImge soundHapticImge;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }



    }

    public void MainMeneScreenLoad()
    {

        LoadSceneWithActionComplete("mainScreen");


    }

    void LoadLevel()
    {
        LoadSceneWithActionComplete("mainScreen");
    }

    public void BackToMainScreen()
    {
        LoadSceneWithActionComplete("mainScreen");
    }

    void LoadSceneWithActionComplete(string st)
    {
        SceneManager.LoadScene(st);
        DestroyImmediate(this.gameObject);
    }
    public void LoadSettingButton(GameObject SettingDialoge)           // x to setting image
    {
        if (SettingDialoge.activeInHierarchy)
        {
            SettingDialoge.SetActive(false);
            soundHapticImge.settingBtnImage.sprite = soundHapticImge.settingbtnSprite;
           
        }

        else
        {

            SettingDialoge.SetActive(true);
            soundHapticImge.settingBtnImage.sprite = soundHapticImge.crossbrnSprite;
          
        }

        SettingDbSpriteSetting();


    }



    #region Sound_Vibration Area

    //====================================================================================================


    public bool IsSettingDialoge;
    public static bool IsSoundOnn = true;
    public static bool IsHapticOnn = true;
    public static bool IsMusicOnn = true;


    public void SoundOnOff()
    {

        if (IsSoundOnn)
        {
            soundHapticImge.soundOnImg.sprite = soundHapticImge.soundOff;
            IsSoundOnn = false;
            Debug.Log(" off ui");


        }
        else
        {
            soundHapticImge.soundOnImg.sprite = soundHapticImge.soundOn;
            IsSoundOnn = true;
            Debug.Log("on ui");

        }

    }

    public GameObject musicOb;
    public void MusicOnOff()
    {

        if (musicOb == null)
        {
            Debug.Log("music");
            return;
        }

        if (IsMusicOnn)
        {
            soundHapticImge.MusicOnImg.sprite = soundHapticImge.musicOff;
            IsMusicOnn = false;

            if (musicOb.GetComponent<AudioSource>())
            {

                musicOb.GetComponent<AudioSource>().enabled = false;
            }


            Debug.Log("music on");
        }
        else
        {
            soundHapticImge.MusicOnImg.sprite = soundHapticImge.musicOn;
            IsMusicOnn = true;

            if (musicOb.GetComponent<AudioSource>())
            {

                musicOb.GetComponent<AudioSource>().enabled = true;
            }
            Debug.Log("music off");
        }

    }


    public HapticTypes hapticTypes = HapticTypes.Selection;

    public void TurnHapticsOnOFF()
    {

        if (!IsHapticOnn)
        {
            Debug.Log("IsHapticOnn in if is " + IsHapticOnn);

            hapticTypes = HapticTypes.Success;
            MMVibrationManager.SetHapticsActive(true);
            MMVibrationManager.Haptic(hapticTypes , false , true , this);

            soundHapticImge.hapticOnImag.sprite = soundHapticImge.HapticOn;
            IsHapticOnn = true;
            Debug.Log("vibration on");
            Debug.Log("on image");


        }
        else
        {
            Debug.Log("IsHapticOnn in else is " + IsHapticOnn);
            hapticTypes = HapticTypes.Warning;
            MMVibrationManager.Haptic(hapticTypes , false , true , this);
            MMVibrationManager.SetHapticsActive(false);
            soundHapticImge.hapticOnImag.sprite = soundHapticImge.HapticOff;
            IsHapticOnn = false;
            Debug.Log("vibration off");
            Debug.Log("off image");

        }
    }

    void SettingDbSpriteSetting()
    {
        if (IsSoundOnn)
        {
            soundHapticImge.soundOnImg.sprite = soundHapticImge.soundOn;
        }
        else
        {
            soundHapticImge.soundOnImg.sprite = soundHapticImge.soundOff;
        }

        /////////////////////////////////////////////////////////////////////////////////////
        //if (IsMusicOnn)
        //{
        //    soundHapticImge.MusicOnImg.sprite = soundHapticImge.musicOn;
        //}                                                                                             
        //else if (!IsMusicOnn)
        //{
        //    soundHapticImge.MusicOnImg.sprite = soundHapticImge.musicOff;
        //}
        ///////////////////////////////////////////////////////////////////////////////////
        if (IsHapticOnn)
        {

            soundHapticImge.hapticOnImag.sprite = soundHapticImge.HapticOn;
            Debug.Log("on image");
        }
        else
        {
            Debug.Log("IsHapticOnn 8 is " + IsHapticOnn);
            soundHapticImge.hapticOnImag.sprite = soundHapticImge.HapticOff;
            Debug.Log("off image");
        }
    }

    public void PlayNiceVibration()
    {
        Debug.Log("hello");
        if (IsHapticOnn)
        {
            Debug.Log("IsHapticOnn 10 is " + IsHapticOnn);
            hapticTypes = HapticTypes.Selection;
            MMVibrationManager.Haptic(hapticTypes);
        }

    }

    #endregion





    [System.Serializable]
    public class SoundHapticImge
    {
        public Sprite soundOn, soundOff;
        [Space(5)]
        [HideInInspector]      // remove later
        public Sprite musicOn;
        [HideInInspector]   // remove later
        public Sprite musicOff;
        [Space(5)]
        public Sprite HapticOn;
        public Sprite HapticOff;
        [Space(5)]
        public Sprite settingbtnSprite, crossbrnSprite;
        [Space(5)]
        [Header("Heirarchi collection ")]
        [Space(10)]
        public Image soundOnImg;
        [HideInInspector]
        public Image MusicOnImg;   // remove later



        public Image hapticOnImag;
        public Image settingBtnImage;
    }


}
