const cityName = document.querySelector('.cityName');
const Date_T = dateFormat(new Date());
const _ = (element) => {
    return document.querySelector(element);
}

function dateFormat(date) {
    let month = date.getMonth() + 1;
    let day = date.getDate();
    let hour = date.getHours();
    let minute = date.getMinutes();
    let second = date.getSeconds();

    month = month >= 10 ? month : '0' + month;
    day = day >= 10 ? day : '0' + day;
    hour = hour >= 10 ? hour : '0' + hour;
    minute = minute >= 10 ? minute : '0' + minute;
    second = second >= 10 ? second : '0' + second;

    return date.getFullYear() + '-' + month + '-' + day + ' ' + hour + ':' + minute + ':' + second;
}

cityName.addEventListener("keypress", (event) => {
    if (event.key === 'Enter') {
        let cityName = event.target.value;
        switch (event.target.value) {
            case '서울':
                cityName = "seoul";
                break;
            case '부산':
                cityName = "busan";
                break;
            case '대구':
                cityName = "daegu";
                break;
            case '광주':
                cityName = "gwangju";
                break;
            case '제주':
                cityName = "jeju";
                break;
            case '아산':
                cityName = "asan";
                break;
            case '천안':
                cityName = "cheonan";
                break;
        }
        _("#weather").textContent = "";
        _("#temp").textContent = `현재 온도 : `;
        _("#temp-min").textContent = `최저 온도 : `;
        _("#temp-max").textContent = `최고 온도 : `;
        _("#temp-feel").textContent = `체감 온도 : `;
        _("#wind").textContent = `풍속 : `;
        weatherBalloon(cityName);
    }
});

function weatherBalloon(cityName) {
    fetch('https://api.openweathermap.org/data/2.5/weather?q=' + cityName + '&appid=ef95dd1b58d9d21b10e7328f915d35ad')
        .then(function (resp) { return resp.json() })
        .then(function (data) {

            _("#weather_save").style.display = "block";

            const K = 273.15;

            _("#weather").textContent += `${(data.weather[0].main)}`;
            _("#temp").textContent += `${(data.main.temp - K).toFixed(1)}`;
            _("#temp-min").textContent += `${(data.main.temp_min - K).toFixed(1)}`;
            _("#temp-max").textContent += `${(data.main.temp_max - K).toFixed(1)}`;
            _("#temp-feel").textContent += `${(data.main.feels_like - K).toFixed(1)}`;
            _("#wind").textContent += `${data.wind.speed}`;

            if (_("#weather").textContent === "Clouds") {
                _("#weather-image").src = "/Content/Images/cloud.svg";
            }
            else if (_("#weather").textContent === "Rain") {
                _("#weather-image").src = "/Content/Images/rain.svg";
            }
            else if (_("#weather").textContent === "Snow") {
                _("#weather-image").src = "/Content/Images/snow.svg";
            }
            else if (_("#weather").textContent === "Clear") {
                _("#weather-image").src = "/Content/Images/clear.svg";
            }
        })
        .catch(function () {
            alert("틀린 도시 이름 입니다. 다시 입력해주세요.")
        });
}


_("#weather_save").addEventListener('click', () => {
    const param = {
        "Region": $(".cityName").val(),
        "Main_Temp": parseFloat($("#temp").text().replace(/^\D+/g, '')),
        "Min_Temp": parseFloat($("#temp-min").text().replace(/^\D+/g, '')),
        "Max_Temp": parseFloat($("#temp-max").text().replace(/^\D+/g, '')),
        "Feel_Temp": parseFloat($("#temp-feel").text().replace(/^\D+/g, '')),
        "Wind": parseFloat($("#wind").text().replace(/^\D+/g, '')),
        "Date_T": Date_T,
    }
    $.ajax({
        url: '/Weathers/AddWeathers',
        type: 'post',
        data: JSON.stringify(param),
        contentType: "application/json",
        success: function (data) {
            alert("등록성공");
        },
        error: function () {
        }
    });
})
