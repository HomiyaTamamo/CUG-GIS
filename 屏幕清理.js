// 清屏功能
    function clearAllEntities() {
      viewer.entities.removeAll();
      document.getElementById("weatherPanel").style.display = "none";
      
      // 更新状态指示器
      const status = document.querySelector('.status-indicator span');
      status.textContent = "所有实体已清除 · 系统运行正常";
      
      // 显示操作提示
      const tip = document.getElementById('operationTip');
      tip.textContent = "所有标记和路径已清除";
      tip.style.display = "block";
      setTimeout(() => { tip.style.display = "none"; }, 3000);
    }