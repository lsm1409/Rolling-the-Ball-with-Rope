using UnityEngine;

/// <summary>
/// Ball controller.
/// Ball 오브젝트에 적용 
/// 조이스틱의 입력을 받아 공의 조작 (이동, 점프 등) 및
/// 로프의 조작 (조준, 발사 등)을 담당하는 스크립트
/// </summary>
/// 
/// 

public class PlayerController : MonoBehaviour
{
    public float JumpPower; // 공 점프시 가하는 힘
    public float MovePower; // 공 이동시 가하는 힘
    public float MaxVelocity; // 공의 최고속도 (공이 이 속도 이하일 때만 힘을 가한다.)
    public float MaxAngularVelocity; // 공의 최고 각속도 (공이 공 중심 기준으로 회전하는 최고 속도)
    public float GroundRayLength; // 공이 땅에 붙어있는지 체크하기 위해 공 중심으로부터 -y방향으로 재는 거리
    public float MaxRopeLength; // 로프가 발사되는 최대 길이

    private new Rigidbody rigidbody;   // 공의 rigidbody 컴포넌트를 담는 필드
    private LineRenderer line; // 로프를 표현하는 LineRenderer 컴포넌트를 담는 필드
    private Joystick moveJoystick; // 공 이동을 조작하는 조이스틱 객체
    private RopeJoyStick ropeJoystick; // 로프를 조작하는 조이스틱 객체
    private Vector3 moveDirection; // 카메라가 바라보는 방향을 고려한 공의 이동방향
    private Vector3 ropeDirection; // 로프 조준 및 발사 방향 (x, y 값만 존재)
    private bool isJumpPressed; // 공의 점프가 입력되면 true
    private bool isRopeAimed;  // 로프 조준이 입력되면 true
    private bool isRopeShot;    // 로프 발사가 입력되면 true
    private bool isConnected;   // 공이 로프와 연결되면 true
    private HingeJoint rope;   // 로프의 joint 정보 (로프가 걸리는 위치, 로프의 길이, 진자 운동 방향, 진자 운동 각도 등)
    private Texture dottedLine;    // 로프의 점선 텍스쳐

    private Vector3 moveDirectionKey;

    private void Start()
    {
        moveJoystick = FindObjectOfType<FloatingJoystick>();   // 오브젝트들 중 FloatingJoyStick 클래스 스크립트가 적용된 오브젝트를 가져온다.
        ropeJoystick = FindObjectOfType<RopeJoyStick>();   // 오브젝트들 중 RopeJoyStick 클래스 스크립트가 적용된 오브젝트를 가져온다.

        rigidbody = GetComponent<Rigidbody>(); // 공의 rigidbody 컴포넌트를 가져온다.
        line = GetComponent<LineRenderer>();   // 공의 LineRenderer 컴포넌트를 가져온다.

        dottedLine = line.material.mainTexture;   // 로프 머티리얼에 적용된 점선 텍스쳐를 저장해둔다.

        GetComponent<Rigidbody>().maxAngularVelocity = MaxAngularVelocity; // 공의 최대 각속도를 지정한 속도로 적용한다.
    }

    private void Update()
    {
        isJumpPressed = ropeJoystick.isJumped; // 로프 조이스틱에서 점프 키를 눌렸는지 여부를 가져온다.
        isRopeAimed = ropeJoystick.isAimed; // 로프 조이스틱이 조준 상태인지 여부를 가져온다.
        isRopeShot = ropeJoystick.isShot;    // 로프 조이스틱에서 로프 발사가 입력되었는지 여부를 가져온다.

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        // 메인 카메라가 바라보는 방향을 기준으로 하여 공의 이동방향을 결정한다.
        moveDirection = (moveJoystick.Vertical * Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized + moveJoystick.Horizontal * Camera.main.transform.right).normalized;

        moveDirectionKey = (v * Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized + h * Camera.main.transform.right).normalized;

        //UI가 켜져있을때 클릭하면 유아이가 꺼짐
        if (Input.GetMouseButtonDown(0))
        {
            if (UIController.tuto_move)
                UIController.tuto_move = false;
            if (UIController.tuto_jump)
            {
                ropeJoystick.isJumped = true;
                UIController.tuto_jump = false;
            }
            if (UIController.tuto_touch)
            {
                ropeJoystick.isJumped = true;
                UIController.tuto_touch = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // -- 공 이동 및 점프 -- //
        // 공에서 -y 방향으로 Raycasting하여 공이 땅에 붙어있다고 판단될 경우 ...
        if (Physics.Raycast(this.transform.position, Vector3.down, GroundRayLength))
        {
            // 최대 제한 속도 안에서 ...
            if (rigidbody.velocity.magnitude <= MaxVelocity)
            {
                // 입력받은 공의 이동방향으로 공에게 힘을 가한다.
                rigidbody.AddForce(moveDirection * MovePower);

                rigidbody.AddForce(moveDirectionKey * MovePower);
            }

            // 점프 키가 눌리면 ...
            if (isJumpPressed)
            {
                // ... +y 방향으로 순간적인 힘을 가한다.
                rigidbody.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            }
        }

        // -- 로프 연결 상태 --
        if (isConnected)
        {
            // ... 로프를 시각적으로 그리기 위해 LineRenderer 컴포넌트를 설정한다. (실선)
            line.enabled = true;   // 컴포넌트를 활성화한다.
            line.SetPosition(0, this.transform.position);  // 선의 시작위치를 공의 위치로 지정한다.
            line.SetPosition(1, rope.connectedBody.transform.TransformPoint(rope.connectedAnchor));  // 선의 끝위치를 로프가 걸린 위치로 지정한다. 로프가 걸린 오브젝트가 움직여도 문제없도록 오브젝트의 local 좌표를 이용하여 구한다.
            line.material.mainTexture = null;  // 점선으로 지정되어있던 선의 텍스쳐를 없앤다.
            line.startWidth = 0.25f;   // 선의 시작 지점 두께
            line.endWidth = 0.25f; // 선의 끝 지점 두께
            line.startColor = new Color(0, 0, 0.2f);   // 선의 시작 지점 색
            line.endColor = new Color(0, 0, 0.2f);     // 선의 끝 지점 색

            rope.anchor = Vector3.Lerp(rope.anchor, new Vector3(0, 0, 0), Time.deltaTime);    // anchor 값을 조정하여 로프가 시간에 따라 서서히 줄어들게 한다. (Lerp를 이용해서 기존 anchor -> (0, 0, 0) 으로)

            // -- 로프 연결 상태에서 점프 입력 --
            if (isJumpPressed)
            {
                // ... 연결 상태를 해제하고 로프를 표현하는 HingeJoint 컴포넌트를 없앤다. (비활성화 불가)
                isConnected = false;
                Destroy(rope);
            }
        }
        // -- 로프 비연결 상태 --
        else
        {
            // (로프가 벽에 걸리는 조건)
            // 공 위치에서 로프 조이스틱 방향으로 로프 최대길이만큼 Raycasting 했을 때 Collider를 갖는 오브젝트가 감지되고,
            // Rigidbody 컴포넌트를 가지고 있으며,
            // Ray가 닿은 표면의 normal 벡터가 +y 방향이 아닐 때 ... (로프를 -y 방향으로 못 쏘게 하기 위해)
            bool canRopeSet = Physics.Raycast(this.transform.position, ropeDirection, out RaycastHit ropeHit, MaxRopeLength) && ropeHit.rigidbody != null && ropeHit.normal != Vector3.up;

            // -- 로프 조준 --
            if (isRopeAimed)
            {
                // ... 로프 조이스틱의 방향을 가져온다. (이 방향으로 로프를 조준하고 발사한다.)
                ropeDirection = ropeJoystick.Direction.normalized;

                // 로프 조준선을 그리기 위해 LineRenderer 컴포넌트를 설정한다. (점선)
                line.enabled = true;   // 컴포넌트를 활성화한다.
                line.SetPosition(0, this.transform.position);  // 선의 시작위치를 공의 위치로 지정한다.
                line.SetPosition(1, this.transform.position + ropeDirection * MaxRopeLength);    // 조준선이기 때문에 공 위치에서 조준방향으로 로프 최대길이만큼 이동한 좌표를 선의 끝 위치로 한다.
                line.material.mainTexture = dottedLine;   // 선의 텍스쳐를 점선으로 한다.
                line.startWidth = 0.1f;    // 선의 시작지점 두께
                line.endWidth = 0.1f;  // 선의 끝지점 두께

                // 로프가 걸릴 수 있는 조건 판단 후 점선 색 지정
                if (canRopeSet)
                {
                    line.startColor = Color.green;
                    line.endColor = Color.green;
                }
                else
                {
                    line.startColor = Color.red;
                    line.endColor = Color.red;
                }
            }
            // -- 로프 조준 상태가 아님 -- (점선 및 실선이 그려질 필요가 없을 때)
            else
            {
                // ... LineRenderder 컴포넌트 비활성화
                line.enabled = false;
            }

            // -- 로프 발사 -- //
            if (isRopeShot && canRopeSet)
            {
                // ... Raycasting하여 닿은 물체의 정보가 ropeHit에 저장된다. 여기서 Ray가 로프라고 보면 된다.
                // ... 로프에 매달리는 효과를 내기 위해 HingeJoint 컴포넌트를 스크립트 상에서 동적 생성한다.
                rope = this.gameObject.AddComponent<HingeJoint>(); // HingeJoint 컴포넌트를 추가하고 그 컴포넌트를 반환받아 객체 rope에 저장한다. rope를 이용해 컴포넌트에 접근/설정 한다.
                rope.connectedBody = ropeHit.rigidbody;   // 로프가 닿은 오브젝트의 rigidbody를 연결한다.
                rope.anchor = transform.InverseTransformPoint(ropeHit.point); // anchor는 공의 위치부터 로프가 닿은 지점까지의 벡터, 즉 로프의 길이이다. 로프가 닿은 위치를 공의 local 좌표로 바꿔 적용한다.
                rope.axis = this.transform.InverseTransformVector(Vector3.forward);    // axis는 진자 운동 방향의 중심축이다.(공의 local 좌표계) 'world 좌표계의 z축 중심 회전' 방향으로 진자 운동 해야 한다.(왼손 법칙)
                rope.autoConfigureConnectedAnchor = false; // 로프가 걸리는 위치를 자동으로 계산하는 걸 막는다.
                rope.connectedAnchor = rope.connectedBody.transform.InverseTransformPoint(ropeHit.point); // connectedAnchor는 로프가 걸리는 위치이다. (로프가 연결된 오브젝트의 local 좌표계)
                rope.enableCollision = true; // 로프로 연결된 오브젝트와의 충돌 허용

                // 로프가 걸린 표면에 따라 진자 운동 각도를 제한하기 위해 JointLimits 객체를 만들어 적용한다.
                JointLimits jointLimits = rope.limits; // 기존 limits 값을 가져온다.
                                                       // 로프가 걸린 표면의 normal 벡터의 방향에 따라 진자 운동 허용 각도를 계산한다.

                if (ropeHit.normal == Vector3.down)
                {
                    // hitAngle = arccos(공과 로프가 걸린 지점의 x축(가로) 거리 / 공과 로프가 걸린 지점(대각선)의 거리)
                    float hitAngle = Mathf.Rad2Deg * Mathf.Acos((ropeHit.point.x - this.transform.position.x) / Vector3.Distance(this.transform.position, ropeHit.point));
                    // 계산한 값 이용하여 최소각, 최대각 지정
                    jointLimits.min = -hitAngle;
                    jointLimits.max = 180 - hitAngle;
                }
                else if (ropeHit.normal == Vector3.left)
                {
                    // hitAngle = arccos(공과 로프가 걸린 지점의 y축(세로) 거리 / 공과 로프가 걸린 지점(대각선)의 거리)
                    float hitAngle = Mathf.Rad2Deg * Mathf.Acos((ropeHit.point.y - this.transform.position.y) / Vector3.Distance(this.transform.position, ropeHit.point));
                    // 계산한 값 이용하여 최소각, 최대각 지정
                    jointLimits.min = hitAngle - 180;
                    jointLimits.max = hitAngle;
                }
                else if (ropeHit.normal == Vector3.right)
                {
                    // hitAngle = arccos(공과 로프가 걸린 지점의 y축(세로) 거리 / 공과 로프가 걸린 지점(대각선)의 거리)
                    float hitAngle = Mathf.Rad2Deg * Mathf.Acos((ropeHit.point.y - this.transform.position.y) / Vector3.Distance(this.transform.position, ropeHit.point));
                    // 계산한 값 이용하여 최소각, 최대각 지정
                    jointLimits.min = -hitAngle;
                    jointLimits.max = 180 - hitAngle;
                }
                // 값 수정한 jointLimits를 적용하고 각도 제한을 허용한다.
                rope.limits = jointLimits;
                rope.useLimits = true;

                // 로프 연결 상태로 변경
                isConnected = true;
            }
        }

        // -- 공의 사망 -- //
        // 공이 압사하거나 추락사하면 지정된 리스폰 포인트에서 부활
        if (Physics.Raycast(this.transform.position, Vector3.up, out RaycastHit hit, 0.35f) || this.transform.position.y < -10)
        {
            transform.position = GameObject.FindWithTag("Respawn" + GameDirector.RespawnPoint.ToString()).transform.position;
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }

        //로프UI가 켜져있을때
        if (UIController.tuto_rope)
        {
            this.rigidbody.velocity = Vector3.zero;
            if (isConnected)
            {
                UIController.tuto_rope = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        int offsetNum = CameraController.offsetNum;

        switch (other.gameObject.tag)
        {
            case "Coin":
                Destroy(other.gameObject);
                break;
            case "BonusCoin":
                Destroy(other.gameObject);
                UIController.GameOver = true;
                break;
            case "Up":
                this.rigidbody.AddForce(new Vector3(1, 2, 0).normalized * 55f, ForceMode.Impulse);
                break;
            case "Finish":
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                UIController.GameOver = true;
                break;
            case "FreezeAll":
                rigidbody.constraints = RigidbodyConstraints.FreezeAll;
                break;
            case "FreezeX":
                rigidbody.constraints = RigidbodyConstraints.FreezePositionX;
                break;
            case "FreezeY":
                rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
                break;
            case "FreezeZ":
                rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
                break;
            case "FreezeNone":
                rigidbody.constraints = RigidbodyConstraints.None;
                break;
            case "RotateL":
                CameraController.offsetNum = (--offsetNum + 4) % 4;
                break;
            case "RotateR":
                CameraController.offsetNum = ++offsetNum % 4;
                break;
            case "Forward":
                CameraController.isForward = true;
                break;
            case "Backward":
                CameraController.isForward = false;
                break;
            case "tuto_jump":
                Destroy(other.gameObject);
                UIController.tuto_jump = true;
                break;
            case "tuto_rope":
                Destroy(other.gameObject);
                UIController.tuto_rope = true;
                break;
            case "tuto_rope_touch":
                Destroy(other.gameObject);
                UIController.tuto_touch = true;
                break;
        }

        // 리스폰 포인트 갱신
        if (other.gameObject.tag == "Respawn" + (GameDirector.RespawnPoint + 1).ToString())
        {
            GameDirector.RespawnPoint++;
        }
    }
}