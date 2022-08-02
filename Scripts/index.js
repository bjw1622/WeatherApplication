const cityName = document.querySelector('.cityName');
const $ = (element) => {
    return document.querySelector(element);
}
cityName.addEventListener("keypress", (event) => {
    if (event.key === 'Enter') {
        const cityName = event.target.value;
        weatherBalloon(cityName);
    }
});

function weatherBalloon(cityName) {
    var key = '{yourkey}';
    fetch('https://api.openweathermap.org/data/2.5/weather?q=' + cityName + '&appid=ef95dd1b58d9d21b10e7328f915d35ad')
        .then(function (resp) { return resp.json() }) // Convert data to json
        .then(function (data) {
            const K = 273.15;
            $("#weather").textContent += `${(data.weather[0].main)}`;
            $("#temp").textContent += `${(data.main.temp - K).toFixed(1)}`;
            $("#temp-min").textContent += `${(data.main.temp_min - K).toFixed(1)}`;
            $("#temp-max").textContent += `${(data.main.temp_max - K).toFixed(1)}`;
            $("#temp-feel").textContent += `${(data.main.feels_like - K).toFixed(1)}`;
            $("#wind").textContent += `${data.wind.speed}`;
            //if (data.weather[0].main === "Rain") {
            //    $("#weather-img").src += "~/Content/Images/smog-solid.svg";
            //}

        })
        .catch(function () {
            // catch any errors
        });
}