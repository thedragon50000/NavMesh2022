# NavMesh2022

本專案為筆記性質，主要記錄 Unity 動畫與導航系統的實作方法（已經升級為Unity6)

## 筆記內容

1. **程式碼控制動畫（無 State）**
   * 需掛載 `Animator` 組件。
   * 無須在 Animator Controller 中手動添加任何 State，純透過代碼驅動。

2. **程式碼控制動畫（含編輯器擴充）**
   * 需掛載 `Animator` 並設置好所有 State。
   * 透過 **State 名稱** 進行呼叫。
   * **Tool 功能**：包含一個編輯器腳本擴充，可將 Animator 內容導出存為 `Scriptable Object`。
   * 支援在動畫進度中插入 **Callback（回呼函式）**。

3. **狀態機與導航系統 (Optional)**
   * 使用狀態機（State Machine）開啟 `NavMeshAgent` 移動功能。
