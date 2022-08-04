const cityName = document.querySelector('.cityName');
const Date_T = new Date().toISOString();
const date = new Date();
const _ = (element) => {
    return document.querySelector(element);
}
cityName.addEventListener("keypress", (event) => {
    if (event.key === 'Enter') {
        const cityName = event.target.value;
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
        .then(function (resp) { return resp.json() }) // Convert data to json
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
            // catch any errors
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
    console.log(param);
    $.ajax({
        url: '/Weathers/AddWeathers',
        type: 'post',
        dataType: "json",
        data: JSON.stringify(param),
        contentType: "application/json",
        success: function (data) {
            alter("등록성공");
        },
        error: function () {
        }
    });
})
