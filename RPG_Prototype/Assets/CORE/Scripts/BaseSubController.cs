using UnityEngine;
using System.Collections;

public class BaseSubController : MonoBehaviour {
    protected GameController GameController {
        get {
            if (gameController == null) {
                gameController = GetComponentInParent<GameController>();
            }
            return gameController;
        }
    }
    private GameController gameController = null;
}
