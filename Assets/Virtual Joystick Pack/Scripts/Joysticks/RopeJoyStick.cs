using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Rope joy stick.
/// Floating Joystick에 기존 스크립트 대신 적용
/// 화면 오른편에 표시되는 조이스틱
/// 로프와 점프를 조작하는 조이스틱에 대한 스크립트
/// </summary>
/// 
/// 
public class RopeJoyStick : Joystick
{
    // 조이스틱을 이용한 입력 여부를 판단하는 bool형 필드
    // BallController 스크립트에서 참조하기 위해 public 변수
    // 조이스틱 핸들을 가운데에 위치한 상태에서 Button Up하면 점프
    // 조이스틱 핸들을 가장자리로 Drag한 상태로 두면 조준 상태
    // 조이스틕 핸들을 가장자리 위치에서 Button Up하면 로프 발사 
    [HideInInspector] public bool isJumped;
    [HideInInspector] public bool isAimed;
    [HideInInspector] public bool isShot;

    Vector2 joystickCenter = Vector2.zero;

    private void Start()
    {
        background.gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        // 가운데 Button Up하여 점프가 입력되면 10 프레임 뒤에 isJumped를 false로 변경
        if (isJumped)
            Invoke("DoNotJump", Time.deltaTime * 10);
        // 가장자리 Button Up하여 로프를 발사하면 한 프레임 뒤에 다시 isShot을 false로 변경
        if (isShot)
            Invoke("DoNotShoot", Time.deltaTime);
    }

    // 조이스틱 핸들을 드래그할 때
    public override void OnDrag(PointerEventData eventData)
    {
        // 조이스틱 핸들을 0.5 이상 이동시켰다면 로프 조준
        isAimed = inputVector.magnitude >= 0.5f ? true : false;

        Vector2 direction = eventData.position - joystickCenter;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }

    // 화면을 터치할 때
    public override void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
        background.position = eventData.position;
        handle.anchoredPosition = Vector2.zero;
        joystickCenter = eventData.position;
    }

    // 화면에서 손을 뗄 때
    public override void OnPointerUp(PointerEventData eventData)
    {
        // 조이스틱 핸들을 0.5 이상 이동시킨 상태에서 손을 뗐다면 로프 발사
        if (inputVector.magnitude >= 0.5f)
        {
            isShot = true;
            isAimed = false;
        }
        // 조이스틱 핸들을 0.5 미만 이동시킨 상태에서 손을 뗐다면 점프 입력
        else
            isJumped = true;

        background.gameObject.SetActive(false);
        inputVector = Vector2.zero;
    }

    // 조이스틱 조작으로 true가 된 필드를 다음 프레임에서 다시 false로 변경하기 위한 함수들
    private void DoNotJump()
    {
        isJumped = false;
    }
    private void DoNotShoot()
    {
        isShot = false;
    }
}
