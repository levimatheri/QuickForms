var surveyId;
window.runSurvey = async (dotNetHelper) => {
    await dotNetHelper.invokeMethodAsync('GetCurrentSurvey').then((surveyObj) => {
       // console.log(JSON.parse(surveyObj).Id);
        surveyId = JSON.parse(surveyObj).Id;
        var survey = new Survey.Model(JSON.parse(surveyObj).Content);
        survey.render("surveyContainer");
        survey.onComplete.add(surveyComplete);
    }).catch(error => {
        console.error(`ERROR! ${error}`);
    });
};

function surveyComplete(sender) {
    console.log(sender.data);
    saveSurveyResults(
        "https://your-web-service.com/" + surveyId,
        sender.data
    )
}

function saveSurveyResults(url, json) {
    const request = new XMLHttpRequest();
    request.open('POST', url);
    request.setRequestHeader('Content-Type', 'application/json;charset=UTF-8');
    request.addEventListener('load', () => {
        // Handle "load"
    });
    request.addEventListener('error', () => {
        // Handle "error"
    });
    request.send(JSON.stringify(json));
}