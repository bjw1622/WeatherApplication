const cityName = document.querySelector('.cityName');
let count = 1;
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
            // 버튼 생성
            // 버튼을 미리 생성 해뒀다면 css변경 
            const K = 273.15;
            _("#weather").textContent += `${(data.weather[0].main)}`;
            _("#temp").textContent += `${(data.main.temp - K).toFixed(1)}`;
            _("#temp-min").textContent += `${(data.main.temp_min - K).toFixed(1)}`;
            _("#temp-max").textContent += `${(data.main.temp_max - K).toFixed(1)}`;
            _("#temp-feel").textContent += `${(data.main.feels_like - K).toFixed(1)}`;
            _("#wind").textContent += `${data.wind.speed}`;
            //if (data.weather[0].main === "Rain") {
            //    $("#weather-img").src += "~/Content/Images/smog-solid.svg";
            //}

        })
        .catch(function () {
            // catch any errors
        });
}


_("#weather_save").addEventListener('click', () => {
    const param = {
        "Main_Temp": parseFloat($("#temp").text().replace(/^\D+/g, '')),
        "Min_Temp": parseFloat($("#temp-min").text().replace(/^\D+/g, '')),
        "Max_Temp": parseFloat($("#temp-max").text().replace(/^\D+/g, '')),
        "Feel_Temp": parseFloat($("#temp-feel").text().replace(/^\D+/g, '')),
        "Wind": parseFloat($("#wind").text().replace(/^\D+/g, '')),
        "Weather_No": count += 1,
    }
    console.log(param);
    $.ajax({
        url: '/Weathers/AddWeathers',
        type: 'post',
        dataType: "json",
        data: JSON.stringify(param),
        contentType:"application/json",
        success: function (data) {
            alter("등록성공");
        },
        error: function () {
        }
    });
})
