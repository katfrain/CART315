using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class RespawnButton : MonoBehaviour
{
    public void OnClick()
    {
        RoomGenerator.Instance.clearRooms();
        SceneLoader.Instance.LoadNewScene(1, () =>
        {
            Player.Instance.transform.position = Vector3.zero;
            RoomGenerator.Instance.levelCount = 0;
            foreach (var shop in RoomGenerator.Instance.Shops)
            {
                shop.Visible = true;
            }
            GameManager.Instance.setLevelText((0).ToString());
            GameOverScreen.Instance.updateLevelText(0);
        });
        GameManager.Instance.MainCamera.transform.position = new Vector3(0, 0, -10);
        Player.Instance.transform.position = Vector3.zero;
        Player.Instance.respawnPlayer();
        GameOverScreen.Instance.Enabled = false;
        Coin.resetScale();
        Turret.resetTurrets();
    }
}
