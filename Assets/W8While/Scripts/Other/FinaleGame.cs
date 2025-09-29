using System.Collections;
using UnityEngine;

public class FinaleGame : MonoBehaviour
{
    [SerializeField] private GameObject YouDiedCanvas;
    [SerializeField] private GameObject _camera;
    [SerializeField] private GameObject _canvas;
    private AudioSource _pvsEndGame;
    private float _cameraSpeed;
    private float _cameraSpeedRotaty;
    private Vector3 _cameraPosition = new Vector3(20f, 12.5f, 16f);
    private Vector3 _cameraRotation = new Vector3(32f, 105f, 0f);
    public bool isEndGame;

    private void Start()
    {
        isEndGame = false;
        _pvsEndGame = GetComponent<AudioSource>();
        _cameraSpeed = 10f;
        _cameraSpeedRotaty = 0.7f;
    }

    public void EndGame()
    {
        if (isEndGame)
            return;
        isEndGame = true;
        AllTowers.DeleteAllTowerSkills();
        StartCoroutine(CameraTransformPosition());
        _canvas.SetActive(false);
        _pvsEndGame.Play();
    }


    private IEnumerator CameraTransformPosition()
    {
        float timer = 0;
        while (timer < 5f)
        {
            _camera.transform.position = Vector3.MoveTowards(_camera.transform.position, _cameraPosition, _cameraSpeed * Time.deltaTime);
            _camera.transform.rotation = Quaternion.Lerp(_camera.transform.rotation, Quaternion.Euler(_cameraRotation), _cameraSpeedRotaty * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        YouDiedCanvas.SetActive(true);
    }
}
