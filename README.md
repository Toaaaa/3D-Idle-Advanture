## 📝 게임 소개
<img src="https://github.com/Toaaaa/3D-Idle-Advanture/blob/f95cbb40c9e77ac6004fa8853463105ac3aa733e/gamepic.PNG?raw=true" width="800">

- 프로젝트명 : 끝없는 협곡
- 게임 컨셉 : 3D 방치형 RPG로, 레퍼런스 게임 "오늘도 우라라" 를 일부 참고함.
- 게임 진행 방식 : 일직선 진행의 자동전투 방식.
- 제작 기간 : 3/20 ~ 3/26

<br />

### 기능 구현
- 기본 UI : 플레이어 상태, 골드, 능력치 UI, 상점 UI, 인벤토리 UI
- 플레이어의 상태 : FSM을 통해 플레이어의 상태를 조절
- 스테이지 선택 : 사전에 저장한 각 맵 청크 데이터를 선택한 스테이지에 맞춰 랜덤하게 스폰.
- 간단한 몬스터 상태 : 간단한 FSM을 통해 행동의 뼈대 제작
- 각종 UI와 이것의 기본적인 기능들 구현
- 데이터 관리 : 스크립터블 오브젝트와 JSON 활용
- 상호작용에 따른 간단한 파티클과 사운드 시스템
- 일부 숫자 (데미지, 골드) BigInteger 사용


<br />

## ⚙ 기술 스택
- C#
- Git


<br />

## 🤔 기술적 이슈와 해결 // 후기
- 맵청크 생성시 위치 어긋남 이슈
    - [트러블 슈팅 - 맵 청크 자동 생성시 틈새 발생](https://toacode.tistory.com/37)
 
- BigInteger와 스크립터블 오브젝트를 활용시 데이터 직렬화시의 이슈
    - [트러블 슈팅 - Biginteger + scriptable object](https://toacode.tistory.com/39)
 
- Action 델리게이트 남용에따른 간단한 느낀점
    - [소규모 개인 게임 회고 - Action의 남용에 대한 반성](https://toacode.tistory.com/40)


<br />
