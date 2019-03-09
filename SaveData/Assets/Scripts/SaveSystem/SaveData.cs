
namespace Game
{
    /// <summary>
    /// Clase que contiene todos los datos de guardado.
    /// Todo se guarda en tipos básicos
    /// </summary>
    [System.Serializable]   //Permite guardar la clase en un fichero
    public class SaveData
    {
        public uint TotalStars; //Puntuacion total
        public uint Gems;  
        public uint[] PowerUps;  

        //Guardamos tipos básicos porque no podemos convertir nuestras propias estructuras (ni de Unity) a binario
        public uint[] Stars;
        public bool[] Blocked;

        public SaveData(uint totalStars, uint gems, uint[] powerUps, uint[] stars, bool[] blocked)
        {
            TotalStars = totalStars;
            Gems = gems;
            PowerUps = powerUps;
            Stars = stars;
            Blocked = blocked;
        }
    }
}
