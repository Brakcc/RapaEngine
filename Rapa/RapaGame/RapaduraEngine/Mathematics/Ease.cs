namespace Rapa.RapaGame.RapaduraEngine.Mathematics;

public static class Ease
{
    #region methodes
    
    public static float Lerp(float a, float b, float t) => a + (b - a) * t;

    public static float QuadraticLerp(float a, float b, float t) => Lerp(a, b, BaseFunctions.QuadraticIn(t));

    private static class BaseFunctions
    {

        #region Quadratic

        public static float QuadraticIn(float t) => t * t;

        #endregion
    }

    #endregion
}