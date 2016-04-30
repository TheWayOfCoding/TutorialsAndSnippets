package thewayofcoding.com.androidsettingsexample;

import android.content.Context;
import android.content.SharedPreferences;

public class Settings {
    public static final String PREFS_NAME = "twoc_settings_example";
    public static final String KEY_SETTINGS_AVAILABLE = "settingsavailable";

    //your custom settings in the application
    public static final String KEY_STATE_VIBRATION = "vibrationstate";

    //the object used to access settings for this application stored in a predefined setting area
    SharedPreferences prefAccessor;

    public Settings(Context parentContext) {
        prefAccessor = parentContext.getSharedPreferences(PREFS_NAME, 0);

        //this code should execute only one when the application is first executed once installed
        if(prefAccessor.contains(KEY_SETTINGS_AVAILABLE)) {
            SharedPreferences.Editor editor = prefAccessor.edit();

            //make it so that we won't need to run this initialization code again
            editor.putString(KEY_SETTINGS_AVAILABLE, "true");

            //make sure preferences exist, if they don't initialize them with defaults
            if(prefAccessor.contains(KEY_STATE_VIBRATION)) {
                editor.putString(KEY_STATE_VIBRATION, "On");
            }

            editor.apply();
        }
    }

    /**
     * return an application setting
     * use the public static KEY_.... constants of this class as the function parameter
     *
     * @param settingKeyName key name from one of the internal constants
     * @return the value or an empty string if not found
     */
    public String getSettingValue(String settingKeyName) {
        return prefAccessor.getString(settingKeyName, "");
    }

    /**
     * set one of the application settings
     * use the public static KEY_.... constants of this class
     * as the function parameter (settingKeyName)
     *
     * @param settingKeyName key name from one of the internal constants
     * @param valueToInsert value to save into that setting
     */
    public void setSettingValue(String settingKeyName, String valueToInsert) {
        SharedPreferences.Editor editor = prefAccessor.edit();
        editor.putString(settingKeyName, valueToInsert);
        editor.apply();
    }

    /***
     * a function to quickly get the vibration setting value
     *
     * @return the setting value
     */
    public boolean isVibrationOn() {
        boolean returnValue = false;
        if(prefAccessor.getString(KEY_STATE_VIBRATION, "").equals("On")) {
            returnValue = true;
        }
        return returnValue;
    }
}
