# 이세계 멸망 디펜스
<details>
<summary><img src="https://img.shields.io/badge/Youtube-FF0000?style=plastic&logo=youtube&logoColor=#FF0000" width="100px"/></summary>

<a href="https://www.youtube.com/watch?v=568Y23r7hUY" target="_blank">
  <img src="https://img.youtube.com/vi/568Y23r7hUY/maxresdefault.jpg" alt="YouTube Video" width="1920px">
</a>
</details><br/><br/><br/>

### 기다리고 있었어, 소환사...

어느 날 갑자기 이세계로 떨어진 내가 소환 능력을 가지게 되었다?!<br/><br/>
# 📦 프로젝트 요약

**사용 툴 & 환경**  
- Unity 2021.3.15f1 (URP)  
- PlayFab  
- Cargold Library  

---

## 🚀 주요 기능

### 1. 아웃라인 쉐이더 (Outline Shader)  
- **문제**: URP 멀티패스 제한으로 Spine 기본 쉐이더 사용 시 비정상 출력  
- **해결**:  
  - Render Feature 우회 적용  
  - 스카이박스 렌더링 이전에 아웃라인 패스 배치 → 그룹 단위 시인성 100% 개선  

### 2. 다이얼로그 시스템 (Dialogue System)  
- **데이터 소스**: Excel / Google Sheet  
- **기능**:  
  - 선택지(Boolean) 분기 처리  
  - Auto / Skip 자동 재생  
  - 밝기 정보 기반 Fade‑In/Out 연출  
- **성과**: 24시간 내 완성, 1차 FGT 1등  

### 3. 퍼포먼스 최적화 (Draw Call & FPS)  
- **툴**: Unity Frame Debugger  
- **분석 대상**: Spine 유닛, 커스텀 멀티패스 쉐이더, UI 캔버스  
- **개선 사항**:  
  - Outline 쉐이더 드로우 콜 2배 이슈 파악  
  - 동적 UI 캔버스 통합 & Sort Order 재구성 → 배치 수 300 → 150 절반 감소  
- **결과**: Galaxy S9 기준 평균 FPS 30 → 45 확보  

### 4. 유닛 소환 툴 (Unit Spawn Tool)  
- **UI**: UGUI 패널 + 검색 기능  
- **Editor 확장**:  
  - `HotSixTool/CreateCharacter` 메뉴 추가  
  - “Main/Test/Challenge” 씬에서만 동작하도록 씬 검증 로직 구현  
- **효과**: 기획·QA팀도 동일 환경에서 원클릭 테스트 가능  

### 5. 액티브 스킬 시스템 (Active Skill System)  
- **패턴**: ScriptableObject 기반 모듈화  
- **입력 분리**: Handler Interface + UniRx MessageBroker  
- **쿨타임 연출**:  
  - Fragment Shader + `atan2`로 시계 방향 쿨타임 표시  
  - Sprite Atlas UV 좌표 보정  

### 6. 아카데미 강화 시스템 (Academy Upgrade System)  
- **협업**: 기획서 검토 후 누락·예외사항 추가 정의  
- **UX 개선**: “강화된 유닛 우선 강화” 로직 제안·적용  
- **구조**: UniRx MessageBroker로 UI/로직 의존성 완전 분리  

### 7. 메모리 풀링 (Memory Pooling)  
- **구현**: `MemoryPoolManager<T>` 제네릭 풀 시스템 + `IPoolable` 인터페이스  
- **효과**: MonoBehaviour 미상속 객체 재사용 → 런타임 GC 부담 완화  

### 8. 캐릭터 이동 (Grid Movement)  
- **입력 처리**: UniRx `UpdateAsObservable()` 스트림으로 터치 업·다운 구분  
- **UX 보정**: 터치가 범위 밖으로 나가도 가장 가까운 칸으로 보정 → 비개발 테스터 불편 피드백 해소  

### 9. 튜토리얼 씬 (Tutorial Scene)  
- **목적**: 비전문가용 조작법 교육  
- **설계**:  
  - 단계별 필요한 UI만 활성화/제거로 의도치 않은 플레이 차단  
  - 기획자와 예외 상황(몬스터 미처치 등) 사전 대응 로직 추가  
- **성과**: 조작법 몰라도 튜토리얼만으로 완전 학습, 테스터 호평  

---
