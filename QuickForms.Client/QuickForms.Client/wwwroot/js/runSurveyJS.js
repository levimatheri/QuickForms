var surveyId;
window.runSurvey = async (dotNetHelper) => {
    await dotNetHelper.invokeMethodAsync('GetCurrentSurvey').then((surveyObj) => {
        surveyId = JSON.parse(surveyObj).Id;
        var survey = new Survey.Model(JSON.parse(surveyObj).Content);
        survey.render("surveyContainer");
        survey.sendResultOnPageNext = true;

        const storageItemKey = `survey-${surveyId}`;

        const prevData = window.localStorage.getItem(storageItemKey) || null;
        if (prevData) {
            const data = JSON.parse(prevData);
            survey.data = data;
            if (data.pageNo) {
                survey.currentPageNo = data.pageNo;
            }
        }
        async function surveyComplete(sender) {
            var surveyResult = {
                surveyId: surveyId,
                surveyResult: JSON.stringify(sender.data)
            }

            // remove partially saved data from local storage
            window.localStorage.removeItem(storageItemKey);

            await dotNetHelper
                .invokeMethodAsync('SaveCurrentSurveyResults', surveyResult).then(() => {
                    // success. resolve callback
                }).catch(error => {
                    console.error(`ERROR! ${error}`);
                });
        }

        function surveyPartiallyComplete(survey) {
            console.log(survey);
            const data = survey.data;
            data.pageNo = survey.currentPageNo;
            window.localStorage.setItem(storageItemKey, JSON.stringify(data));
        }

        // Save partial survey results
        survey.onPartialSend.add((survey) => {
            console.log("HERE");
            surveyPartiallyComplete(survey);
        });

        // Save complete survey results
        survey.onComplete.add(surveyComplete);
    }).catch(error => {
        console.error(`ERROR! ${error}`);
    });
};