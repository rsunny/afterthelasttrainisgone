using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public static class ManageGame {
    private static string savedName;
    private static int savedHealth;
    private static int savedState;
    private static Vector3 savedPosition;

    public static void setSceneName(string name)
    {
        savedName = name;
    }

    public static void setHealth(int health)
    {
        savedHealth = health;
    }

    public static void setState(int state)
    {
        savedState = state;
    }

    public static void setPosition(Vector3 pos)
    {
        savedPosition = pos;
    }

    public static string getSceneName()
    {
        return savedName;
    }

    public static int getHealth()
    {
        return savedHealth;
    }

    public static int getState()
    {
        return savedState;
    }

    public static Vector3 getPosition()
    {
        return savedPosition;
    }


    public static void Save()
    {
        Debug.Log("In Save");
        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log(Application.persistentDataPath + "/FileName.dat");
        FileStream file = File.Open(Application.persistentDataPath + "/FileName.dat", FileMode.Create);
        GameStatistics newData = new GameStatistics();
        newData.health = savedHealth;
        newData.scene_name = savedName;
        newData.state = savedState;
        newData.position_x = (int)savedPosition.x;
        newData.position_y = (int)savedPosition.y;
        newData.position_z = (int)savedPosition.z;
        Debug.Log(newData);
        bf.Serialize(file, newData);
        file.Close();
    }

    public static void Load()
    {

        if (File.Exists(Application.persistentDataPath + "/FileName.dat"))
        {
            Debug.Log("In ManageGames Load");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/FileName.dat", FileMode.Open);
            GameStatistics newData = (GameStatistics)bf.Deserialize(file);
            Debug.Log(newData);
            file.Close();
            ManageGame.setHealth(newData.health);
            ManageGame.setSceneName(newData.scene_name);
            ManageGame.setState(newData.state);
            Vector3 pos;
            pos.x = newData.position_x;
            pos.y = newData.position_y;
            pos.z = newData.position_z;
            ManageGame.setPosition(pos);
        }
    }

    [Serializable]

    class GameStatistics
    {
        public string scene_name;
        public int health;
        public int state;
        public int position_x;
        public int position_y;
        public int position_z;
    }
}