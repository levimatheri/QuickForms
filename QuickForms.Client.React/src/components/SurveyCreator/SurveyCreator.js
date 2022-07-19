import React from "react";
import { SurveyCreatorComponent, SurveyCreator } from "survey-creator-react";
import "survey-core/defaultV2.min.css";
import "survey-creator-core/survey-creator-core.min.css";

const creatorOptions = {
    showLogicTab: true,
    isAutoSave: true
};

const defaultJson = {
    pages: [{
        name: "Name",
        elements: [{
            name: "FirstName",
            title: "Enter your first name:",
            type: "text"
        }, {
            name: "LastName",
            title: "Enter your last name:",
            type: "text"
        }]
    }]
};

export function SurveyCreatorWidget() {
    const creator = new SurveyCreator(creatorOptions);
    creator.text = window.localStorage.getItem("survey-json") || JSON.stringify(defaultJson);
    creator.saveSurveyFunc = (saveNo, callback) => {
        // window.localStorage.setItem("survey-json", creator.text);
        // callback(saveNo, true);

        saveSurveyJson(
            "http://localhost:5122/api/Survey/",
            creator.JSON,
            saveNo,
            callback
        );
    };

    return (
        <SurveyCreatorComponent creator={creator} />
    )
}

function saveSurveyJson(url, json, saveNo, callback) {
  const request = new XMLHttpRequest();
  request.open('POST', url);
  request.setRequestHeader('Content-Type', 'application/json;charset=UTF-8');
  request.addEventListener('load', () => {
      callback(saveNo, true);
  });
  request.addEventListener('error', () => {
      callback(saveNo, false);
  });
  request.send(JSON.stringify(json));
}