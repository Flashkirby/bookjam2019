using UnityEngine;

public class FactBookButton : MonoBehaviour
{
    public void ToggleFactBook()
    {
        Game.S.factBook.ToggleFactBook();
    }
}
