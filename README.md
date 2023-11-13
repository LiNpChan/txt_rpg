# txt_rpg
Text Rpg 기능 구현

## 주요기능
### 메인화면
- 게임 실행시 짧은 스토리 인트로 실행
- 캐릭터 상태, 인벤토리, 게임종료 메뉴를 보여주며 각 메뉴가 동작함

### 캐릭터 상태 확인
- 현재 캐릭터의 스탯, 체력, 소유 골드량 등을 화면에 나타냅니다
- 만약 아이템을 통한 추가적인 스탯 증가량이 있을시 스탯이 업데이트 됩니다

### 캐릭터 인벤토리
- 캐릭터가 보유중인 인벤토리를 보여주며 장착관리탭을 통해서 아이템을 장착할 수 있습니다.
- 만일 장착중인 아이템이 있을시 "[E]" 심볼로 표기해 눈으로 확인하기 쉬우며 장착 해제 중 일때는 "[]" 심볼로 표기됩니다

### 아이템 장착 관리
- 캐릭터가 보유한 아이템의 장착 상태를 설정할 수 있습니다.

### 버그 및 추가 개선 사항
- 장착관리에서만 아이템 장착 상태를 관리할 수 있게 하기(현재 인벤토리에서 '장착'만 가능함 / 장착 관리에서는 정상 작동)
- 장착중인 아이템을 선택시 "이미 장비한 아이템입니다" 메세지가 나타나지 않는 오류
- 아이템 추가하기(완료)
- 각 부위에 장비 하나만 장착 가능하게 하기(sword에 1개, armor에 1개씩만)
