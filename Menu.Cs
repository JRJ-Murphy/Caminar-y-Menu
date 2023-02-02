public void PlayGame(){
    sceneManager.LoadScene(sceneManager.GetActiveScene().buildindex + 1);
    // Esto lo que hace es que le dice al programa que active la escena siguiente en la que estamos, se puede hacer de la siguiente manera tambien:
    
    // sceneManager.LoadScene(4); 
    // sceneManager.LoadScene("PAPITO");
}

public void QuitGame(){
    Application.Quit(); 
    // Esto es para quitar el juego. Por si acaso no se entendio. 
}

// con esto ya tienen dos funciones del menu, para el repositorio.

