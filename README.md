# 🧠 DDD 五子棋架構 (Gomoku with DDD & Orleans)

本專案是一個採用 [領域驅動設計（DDD）](https://en.wikipedia.org/wiki/Domain-driven_design) 原則實作的五子棋遊戲系統，符合 SOLID 原則，並使用微服務 Actor 模型框架 [Microsoft Orleans](https://learn.microsoft.com/en-us/dotnet/orleans/) 部署於 Kubernetes 上，支援 Auto Scaling 與高可用性。

---

## 🧱 架構特色

### ✅ 使用 DDD 模式建構模組邊界

- **Domain Layer**  
  定義核心商業邏輯與 Value Objects，如：
  - `GameId`, `Player`, `Board`, `Coordinate`, `Stone`, `WinRule`
  - `GameAggregate` 管理遊戲狀態變化與下棋流程

- **Application Layer**
  負責處理用戶指令（Command）與查詢（Query），調用 Domain 並包裝輸出結果。
  - `StartGameCommand`, `PlaceStoneCommand`
  - `GetGameStatusQuery`

欲了解更完整的設計細節，請參考 [docs/Design.md](docs/Design.md)。

- **Infrastructure Layer**  
  提供外部服務實作，例如：
  - Orleans Grain 介接
  - Redis / SQL 儲存快照（Snapshot）
  - Logging、Monitoring、Metrics

- **Presentation Layer**  
  API 端點與 Orleans Grain 的橋接，支援 WebSocket 或 RESTful API。

### 🧪 遵守 SOLID 原則

- **S**：每個類別僅關注單一職責（如 `GameRulesService`）
- **O**：遊戲規則支援擴充不同玩法（如自由五子棋 / 禁手規則）
- **L**：可以替換棋盤檢查策略實作，不影響呼叫方
- **I**：細分 interface，例如 `IMoveValidator`, `IGameOverDetector`
- **D**：透過 DI 注入 Grain、服務、Repository

---

## ⚙️ Orleans on Kubernetes

### ☁️ Auto Scaling 支援

- 使用 Orleans 的 **Virtual Actor Model**，每個遊戲對戰即為一個獨立的 `GameGrain`。
- Grains 無狀態時可被自動 GC，節省資源。
- 部署於 K8s 上使用 HPA（Horizontal Pod Autoscaler）自動擴充 Orleans Silo 實例。
- 可與 Redis / SQL 結合實現 Orleans Persistence。

### 🌐 Orleans 架構總覽

```plaintext
[Client (Web/API)]
       |
[GameGrain : IGameGrain]
       |
[GameAggregate (DDD)]
       |
[GameRepository] ← Snapshot / Event Store / Redis
```

### 🚀 快速開始

#### 開發環境需求

- .NET 8 SDK
- Docker
- Kubernetes (Minikube 或 K3s)
- Redis 或 SQL Server

#### 啟動方式（開發）

```bash
dotnet run --project src/Gomoku.Api
```

#### 啟動方式（Kubernetes）

```bash
kubectl apply -f k8s/orleans-deployment.yaml
kubectl apply -f k8s/orleans-service.yaml
```

Orleans Dashboards 與 Prometheus 可支援監控 Actor 狀態。

### 📦 未來規劃

- 支援多種開局規則與禁手邏輯
- 加入觀戰模式（使用 Orleans Streams）
- 實作 Event Sourcing 儲存對戰歷史
- 部署至 Azure AKS / GCP GKE
