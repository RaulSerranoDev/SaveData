using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Game
{
    /// <summary>
    /// Sistema de guardado del juego
    /// </summary>
    public static class SaveSystem
    {
        /// <summary>
        /// Ruta donde se guarda. Usa una ruta propia del sistema operativo donde se ejecuta
        /// </summary>
        private static string path = Application.persistentDataPath + "/game.save";

        /// <summary>
        /// Guarda el estado del juego en un fichero serializado
        /// </summary>
        public static void SaveGame()
        {
            //Creamos un BinaryFormatter
            BinaryFormatter formatter = new BinaryFormatter();

            //Para leer /escribir en el archivo
            FileStream stream = new FileStream(path, FileMode.Create);

            //Convierte la clase a binario
            formatter.Serialize(stream, SaveData());

            //Hay que cerrarlo siempre
            stream.Close();
        }

        /// <summary>
        /// Comportamiento especifico de guardar datos de juego
        /// </summary>
        private static SaveData SaveData()
        {
            uint totalStars = GameManager.Instance.TotalStars;
            uint gems = GameManager.Instance.Gems;
            uint[] powerUps = GameManager.Instance.PowerUps;

            List<LevelInfo> levelInfo = GameManager.Instance.LevelInfo;

            //Covnersión de datos
            uint[] stars = new uint[levelInfo.Count];
            bool[] blocked = new bool[levelInfo.Count];

            for (int i = 0; i < levelInfo.Count; i++)
            {
                stars[i] = levelInfo[i].Stars;
                blocked[i] = levelInfo[i].Blocked;
            }

            return new SaveData(totalStars, gems, powerUps, stars, blocked);
        }
        
        /// <summary>
        /// Carga el fichero serializado
        /// Devuelve si ha cargado correctamente
        /// </summary>
        /// <returns></returns>
        public static bool LoadGame()
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                SaveData saveDataGame = formatter.Deserialize(stream) as SaveData;
                stream.Close();

                LoadData(saveDataGame);

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Comportamiento especifico de cargar los datos del juego
        /// </summary>
        /// <param name="saveDataGame"></param>
        private static void LoadData(SaveData saveDataGame)
        {
            GameManager.Instance.TotalStars = saveDataGame.TotalStars;
            GameManager.Instance.Gems = saveDataGame.Gems;
            GameManager.Instance.PowerUps = saveDataGame.PowerUps;

            GameManager.Instance.LevelInfo = new List<LevelInfo>();

            for (int i = 0; i < saveDataGame.Stars.Length; i++)
            {
                LevelInfo levelInfo = new LevelInfo();
                levelInfo.Stars = saveDataGame.Stars[i];
                levelInfo.Blocked = saveDataGame.Blocked[i];

                GameManager.Instance.LevelInfo.Add(levelInfo);
            }
        }

        /// <summary>
        /// Resetea el archivo de guardado al por defecto y lo guarda
        /// </summary>
        public static void ResetSave()
        {
            GameManager.Instance.TotalStars = 0;
            GameManager.Instance.Gems = 200;

            uint numPowerUps = 4;
            GameManager.Instance.PowerUps = new uint[numPowerUps];

            for (int i = 0; i < GameManager.Instance.PowerUps.Length; i++)
                GameManager.Instance.PowerUps[i] = 2;

            GameManager.Instance.LevelInfo = new List<LevelInfo>();

            for (int i = 0; i <10; i++)
            {
                LevelInfo levelInfo = new LevelInfo();
                levelInfo.Stars = 0;
                levelInfo.Blocked = true;

                GameManager.Instance.LevelInfo.Add(levelInfo);
            }

            GameManager.Instance.LevelInfo[0].Blocked = false;

            SaveGame();
        }

        /// <summary>
        /// Borra los archivos de guardado
        /// </summary>
        public static void DeleteData()
        {
            File.Delete(path);
        }
    }
}