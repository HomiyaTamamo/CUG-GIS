    // 获取天气数据
    function fetchWeather(name, lon, lat) {
      const apiKey = 'a4493170053647c0ae02a7e69f21f08b';
      const host = 'p32yvwmb3k.re.qweatherapi.com';
      const url = `https://${host}/v7/weather/3d?location=${lon},${lat}&key=${apiKey}`;
      
      fetch(url).then(res => res.json()).then(data => {
        if (data.code === "200") {
          showWeather(name, data.daily);
        } else {
          showWeather(name, [], true);
        }
      }).catch(err => {
        console.error(err);
        showWeather(name, [], true);
      });
    }

    // 显示天气信息
    function showWeather(name, dailyData, error = false) {
      const panel = document.getElementById("weatherPanel");
      const title = document.getElementById("weatherTitle");
      const content = document.getElementById("weatherContent");
      
      title.textContent = `${name} 天气预报`;
      
      if (error || !dailyData.length) {
        content.innerHTML = `<p style="color:red; text-align:center; padding:10px 0;">无法获取天气数据，请稍后再试。</p>`;
      } else {
        content.innerHTML = dailyData.map(day => `
          <div class="weatherDay">
            <div class="day-name">${day.fxDate}</div>
            <img src="https://icons.qweather.com/assets/icons/${day.iconDay}.svg" alt="${day.textDay}">
            <div class="weatherInfo">
              <strong>${day.textDay}</strong><br>
              ${day.tempMin}℃ ~ ${day.tempMax}℃<br>
              ${day.windDirDay} ${day.windScaleDay}级
            </div>
          </div>
        `).join('');
      }
      
      panel.style.display = "block";
    }