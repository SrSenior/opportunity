public interface IAplicableAPuerta
{
    void AplicarADoor(DoorData puerta);
}

/*
 * Esta es una interfaz
 * la idea es como "okay, de ahora en adelante, todas las clases que tengan esta interfaz
 * debe tener estas funciones o comportamientos"
 * 
 * Es decir, si ponemos de ejemplo la interfaz de IPointerClickHandler
 * es como decir "okay, hiciste click en un elemento con la interfaz de IPointerClickHandler?
 * entonces, en ese caso, haremos lo que indiques en el script de ese elemento al que clickeaste"
 * Digamos que, en ese caso, el clickear en el elemento es lo que activa su comportamiento particular
 * ante dicho evento: ser clickeado (valga la redundancia)
 * 
 * En resumen, invocas el evento que "activa" la interfaz, entonces en un script tendrás un comportamiento
 * definido para ese evento particular.
 */