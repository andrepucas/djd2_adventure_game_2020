using UnityEngine;
using UnityEngine.Rendering;
public class MemoryTravel : MonoBehaviour

{
    private CharacterController _controller;
    private Vector3 _distance;
    private bool _inMemoryTravel;

    private RenderPipelineAsset _UniversalRenderPipelineAsset;
    //private RenderPipelineAsset _UniversalRenderPipelineAsset2;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _inMemoryTravel = false;
    }

    void Update()
    {
        CheckForMemoryTravel();

        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     GraphicsSettings.renderPipelineAsset = 
        //     _UniversalRenderPipelineAsset1;

        // }
        //     else if (Input.GetKeyDown(KeyCode.Space)) {
        //     GraphicsSettings.renderPipelineAsset = 
        //     _UniversalRenderPipelineAsset2;

        // }
    }

    private void CheckForMemoryTravel()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_inMemoryTravel)
            {
                _distance = new Vector3(-15, 0, 0);

            }
            else
            {
                _distance = new Vector3(15, 0, 0);

            }

            Teleport();
        }
    }

    private void Teleport()
    {

        _controller.Move(transform.TransformVector(_distance));

        _inMemoryTravel = !_inMemoryTravel;
         GraphicsSettings.renderPipelineAsset = _UniversalRenderPipelineAsset;

        Debug.Log("Memory Travelled.");
        Debug.Log($"MemoryTravel: {_inMemoryTravel}");
        Debug.Log("Active render pipeline asset is: " +
        GraphicsSettings.renderPipelineAsset.name);
    }
}
