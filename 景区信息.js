
// 初始化位置数据
    const locations = {
      "故宫": { lon: 116.390872, lat: 39.915855, height: 800 },
      "西湖": { lon: 120.1500, lat: 30.2435, height: 1000 },
      "九寨沟": { lon: 103.9183, lat: 33.2677, height: 2500 },
      "东方明珠": { lon: 121.49517, lat: 31.24194, height: 800 },
      "布达拉宫": { lon: 91.1175, lat: 29.6578, height: 1500 },
    };

    const routeData = {
  "故宫": {
    distance: "8.5公里",
    startStation: "北京站",
    transport: [
      {
        type: "地铁线路",
        icon: "fas fa-subway",
        time: "约45分钟",
        price: "￥5",
        color: "#34a853",
        details: "乘坐地铁2号线至前门站，换乘1号线至天安门东站"
      },
      {
        type: "公交路线",
        icon: "fas fa-bus",
        time: "约60分钟",
        price: "￥2",
        color: "#1a73e8",
        details: "北京站东公交站乘坐20路至天安门东站"
      },
      {
        type: "出租车/网约车",
        icon: "fas fa-taxi",
        time: "约30分钟",
        price: "￥35-45",
        color: "#9c27b0",
        details: "北京站打车直达故宫东华门"
      },
      {
        type: "自行车/步行",
        icon: "fas fa-bicycle",
        time: "约90分钟",
        price: "免费",
        color: "#ff9800",
        details: "从北京站沿长安街骑行/步行至故宫"
      }
    ]
  },
  "西湖": {
    distance: "12公里",
    startStation: "杭州站",
    transport: [
      {
        type: "地铁线路",
        icon: "fas fa-subway",
        time: "约35分钟",
        price: "￥6",
        color: "#34a853",
        details: "杭州站乘坐地铁1号线至龙翔桥站，步行至西湖"
      },
      {
        type: "公交路线",
        icon: "fas fa-bus",
        time: "约50分钟",
        price: "￥2",
        color: "#1a73e8",
        details: "杭州站公交站乘坐K7路至西湖景区"
      },
      {
        type: "水上巴士",
        icon: "fas fa-ship",
        time: "约40分钟",
        price: "￥15",
        color: "#00bcd4",
        details: "从武林门码头乘船至西湖景区"
      },
      {
        type: "出租车/网约车",
        icon: "fas fa-taxi",
        time: "约25分钟",
        price: "￥30-40",
        color: "#9c27b0",
        details: "杭州站打车直达西湖断桥"
      }
    ]
  },
  "布达拉宫": {
    distance: "25公里",
    startStation: "拉萨站",
    transport: [
      {
        type: "旅游专线",
        icon: "fas fa-bus",
        time: "约70分钟",
        price: "￥10",
        color: "#1a73e8",
        details: "拉萨站乘坐旅游专线1号线直达布达拉宫广场"
      },
      {
        type: "出租车/网约车",
        icon: "fas fa-taxi",
        time: "约45分钟",
        price: "￥60-80",
        color: "#9c27b0",
        details: "拉萨站打车直达布达拉宫"
      },
      {
        type: "机场巴士",
        icon: "fas fa-shuttle-van",
        time: "约60分钟",
        price: "￥25",
        color: "#ff5722",
        details: "拉萨站乘坐机场巴士至布达拉宫"
      }
    ]
  },
  "九寨沟": {
    distance: "85公里",
    startStation: "九寨沟站",
    transport: [
      {
        type: "景区巴士",
        icon: "fas fa-bus",
        time: "约90分钟",
        price: "￥45",
        color: "#1a73e8",
        details: "九寨沟站乘坐景区直达巴士"
      },
      {
        type: "出租车/包车",
        icon: "fas fa-taxi",
        time: "约75分钟",
        price: "￥200-300",
        color: "#9c27b0",
        details: "九寨沟站包车前往景区"
      }
    ]
  },
  "东方明珠": {
    distance: "15公里",
    startStation: "上海站",
    transport: [
      {
        type: "地铁线路",
        icon: "fas fa-subway",
        time: "约35分钟",
        price: "￥4",
        color: "#34a853",
        details: "上海站乘坐地铁1号线至人民广场，换乘2号线至陆家嘴"
      },
      {
        type: "观光巴士",
        icon: "fas fa-bus",
        time: "约60分钟",
        price: "￥30",
        color: "#1a73e8",
        details: "上海站乘坐都市观光巴士1号线直达"
      },
      {
        type: "出租车/网约车",
        icon: "fas fa-taxi",
        time: "约40分钟",
        price: "￥50-65",
        color: "#9c27b0",
        details: "上海站打车直达东方明珠"
      },
      {
        type: "轮渡+地铁",
        icon: "fas fa-ship",
        time: "约50分钟",
        price: "￥8",
        color: "#00bcd4",
        details: "地铁至东昌路渡口，乘轮渡至陆家嘴"
      }
    ]
  }
};
 // 打开介绍页面
    function openIntroducePage(name) {
      const urlMap = {
        "故宫": "https://www.dpm.org.cn/Home.html",
        "西湖": "https://wgly.hangzhou.gov.cn/cn/whhz/sjyc/xh/index.html",
        "九寨沟": "https://www.jiuzhai.com/intelligent-service/way-of-play",
        "东方明珠": "https://www.biwuke.com/dfmz/index.html",
        "布达拉宫": "https://www.potalapalace.cn/"
      };
      
      const url = urlMap[name];
      if (url) {
        window.open(url, "_blank");
      } else {
        alert("未找到该景点介绍网页");
      }
      
      // 关闭介绍面板
      document.getElementById("introducePanel").style.display = "none";
    }
  // 打开买票页面
    function openTicketPage(name) {
      const urlMap = {
        "故宫": "https://ticket.dpm.org.cn/",
        "西湖": "",
        "九寨沟": "https://www.jiuzhai.com/intelligent-service/tickets",
        "东方明珠": "https://www.klook.com/zh-CN/activity/3947-oriental-pearl-tower-shanghai/",
        "布达拉宫": "https://www.potalapalace.cn/ticket.html"
      };
      
      const url = urlMap[name];
      if (url) {
        window.open(url, "_blank");
      } else {
        alert("这是个免费景点");
      }
      
      // 关闭介绍面板
      document.getElementById("toggleTicketPanel").style.display = "none";
    }