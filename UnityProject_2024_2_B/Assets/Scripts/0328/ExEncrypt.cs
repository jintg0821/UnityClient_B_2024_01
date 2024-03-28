using Newtonsoft.Json;  //JSON 직렬화를 위한 패키지
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;       
using System;
using System.Linq;

public class ExEncrypt : MonoBehaviour
{
    string filePath;
    string key = "ThisIsASecretKey";    //암호화키

    // Start is called before the first frame update
    void Start()
    {
        filePath = Application.persistentDataPath + "/EncryptplayerData.json";
        Debug.Log(filePath);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerData playerData = new PlayerData();
            playerData.playerName = "플레이어 1";
            playerData.playerLevel = 1;
            playerData.items.Add("돌1");
            playerData.items.Add("바위1");
            SaveData(playerData);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerData playerData = new PlayerData();

            playerData = LoadData();

            Debug.Log(playerData.playerName);
            Debug.Log(playerData.playerLevel);
            for (int i = 0; i < playerData.items.Count; i++)
            {
                Debug.Log(playerData.items[i]);
            }
        }
    }

    void SaveData(PlayerData data)
    {
        //JSON 직렬화
        string jsonData = JsonConvert.SerializeObject(data);

        //데이터를 바이트 배열로 변환
        byte[] bytesToEncrypt = Encoding.UTF8.GetBytes(jsonData);

        //암호화
        byte[] encryptedBytes = Encrypt(bytesToEncrypt);

        //암호화된 데이터를 Base64 문자열로 변환
        string encryptedData = Convert.ToBase64String(encryptedBytes);

        //파일 저장
        File.WriteAllText(filePath, encryptedData);
    }

    PlayerData LoadData()
    {
        if (File.Exists(filePath))
        {
            //파일에서 데이터 읽기
            //string jsonData = File.ReadAllText(filePath);

            //파일에서 암호화 된 데이터 읽기
            string encryptedData = File.ReadAllText(filePath);

            //Base64 문자열을 바이트 배열로 변환
            byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

            //복호화
            byte[] decryptedBytes = Decrypt(encryptedBytes);

            //바이트 배열을 문자열로 변환
            string jsonData = Encoding.UTF8.GetString(decryptedBytes);

            //JSON 역직렬화
            PlayerData data = JsonConvert.DeserializeObject<PlayerData>(jsonData);
            return data;
        }
        else
        {
            return null;
        }
    }

    byte[] Encrypt(byte[] plainBytes)
    {
        using (Aes aseAig = Aes.Create())
        {
            aseAig.Key = Encoding.UTF8.GetBytes(key);
            aseAig.IV = new byte[16];   //IV (intialization Vector) 랜덤값을 사용하거나 고정값을 설정

            //암호화 변환기 생성
            ICryptoTransform encryptor = aseAig.CreateEncryptor(aseAig.Key, aseAig.IV);

            //스트림 생성
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                //스트림에 암호화 변환기를 연결하여 암호화 스트림을 생성
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    //암호화 스트림에 데이터 쓰기
                    csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                    csEncrypt.FlushFinalBlock();

                    //암호화된 데이터 바이트를 배열로 반환
                    return msEncrypt.ToArray();
                }
            }
        }
    }

    byte[] Decrypt(byte[] encryptedBytes)
    {
        using (Aes aseAig = Aes.Create())
        {
            aseAig.Key = Encoding.UTF8.GetBytes(key);
            aseAig.IV = new byte[16];   //IV (intialization Vector) 랜덤값을 사용하거나 고정값을 설정

            //복호화 변환기 생성
            ICryptoTransform decryptor = aseAig.CreateDecryptor(aseAig.Key, aseAig.IV);

            //스트림 생성
            using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
            {
                //스트림에 복호화 변환기를 연결하여 복호화 스트림을 생성
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    //복호화된 데이터를 담을 바이트 배열 생성
                    byte[] decryptedBytes = new byte[encryptedBytes.Length];

                    //복호화 스트림에서 데이터를 읽기
                    int decryptedByteCount = csDecrypt.Read(decryptedBytes, 0, decryptedBytes.Length);

                    //실제로 읽힌 크기만큼의 바이트 배열

                    return decryptedBytes.Take(decryptedByteCount).ToArray();
                }
            }
        }
    }
}
