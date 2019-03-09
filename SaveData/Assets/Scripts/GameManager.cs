using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// Clase persistente entre escenas
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        /// <summary>
        /// Número de gemas actuales
        /// </summary>
        public uint Gems;

        /// <summary>
        /// Número total de estrellas conseguidas
        /// </summary>
        public uint TotalStars;

        /// <summary>
        /// Array con los powerUps disponibles
        /// </summary>
        public uint[] PowerUps;

        /// <summary>
        /// Lista con la información de los niveles
        /// </summary>
        public List<LevelInfo> LevelInfo;

        /// <summary>
        /// Persistent Singleton
        /// Carga archivo de guardado o crea uno por defecto
        /// </summary>
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                if (!SaveSystem.LoadGame())
                    SaveSystem.ResetSave();
  
            }
            else
                Destroy(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                SaveSystem.LoadGame();
            if (Input.GetKeyDown(KeyCode.W))
                SaveSystem.SaveGame();
            if (Input.GetKeyDown(KeyCode.E))
                SaveSystem.ResetSave();
            if (Input.GetKeyDown(KeyCode.R))
                SaveSystem.DeleteData();
            if (Input.GetKeyDown(KeyCode.A))
                Gems++;
            if (Input.GetKeyDown(KeyCode.S))
                TotalStars++;
            if (Input.GetKeyDown(KeyCode.D))
                LevelInfo[0].Stars++;
        }
    }
}