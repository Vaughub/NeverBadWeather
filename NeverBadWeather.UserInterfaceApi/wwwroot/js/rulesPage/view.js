appContext.view.add('rules', function () {
    const model = appContext.model;
    const ruleEdit = model.inputs.ruleEdit;
    const temperature = ruleEdit.temperature;
    const weatherType = ruleEdit.weatherType || 'both';
    const weatherTypes = model.weatherTypes;
    const updateText = ruleEdit.id === null ? 'Legg til ny regel' : 'Oppdater regel';
    const clothes = ruleEdit.clothes || '';
    document.getElementById('app').innerHTML = `
        <small><a href="javascript:goTo('main')">Få klesanbefaling!</a></small>
        <hr/>
        <h3>Aldri dårlig vær!</h3>
        <button  onclick="createRule()">Ny regel</button>
        <table>
            <tr>
                <th>Fra</th>
                <th>Til</th>
                <th>Værtype</th>
                <th>Klær</th>
            </tr>
            ${appContext.model.rules.map((rule,i) => `
            <tr>
                <td>${rule.fromTemperature}°C</td>
                <td>${rule.toTemperature}°C</td>
                <td>${weatherTypeText(rule.isRaining)}</td>
                <td>${rule.clothes}</td>
                <td>
                    <a href="javascript:selectRule(${i})">velg</a>
                    <a href="javascript:deleteRule(${i})">slett</a>
                </td>
            </tr>
            `).join('')}
        </table>
        
        <hr/>
        For temperaturer mellom 
        <span class="timeStepUpDown" onclick="changeTemperature('from',-1)">▼</span
        ><span class="timeStepUpDown">${temperature.from}°C</span
        ><span class="timeStepUpDown" onclick="changeTemperature('from',+1)">▲</span>
        og
        <span class="timeStepUpDown" onclick="changeTemperature('to',-1)">▼</span
        ><span class="timeStepUpDown">${temperature.to}°C</span
        ><span class="timeStepUpDown" onclick="changeTemperature('to',+1)">▲</span>
        for
        <select onchange="appContext.model.inputs.ruleEdit.weatherType=this.value">
            ${Array.from(Object.keys(weatherTypes)).map(wt => `
                <option ${wt === weatherType ? 'selected' : ''} value="${wt}">${weatherTypes[wt]}</option>
            `).join('')}
        </select>
        anbefales følgende klær: 
        <input type="text" oninput="appContext.model.inputs.ruleEdit.clothes=this.value" value="${clothes}" />

        <br/>
        <button onclick="updateRule()">${updateText}</button>        
        <br/>
    `;
});


function weatherTypeText(isRaining) {
    const key = weatherTypeKeyFromIsRaining(isRaining);
    const weatherTypes = appContext.model.weatherTypes;
    return weatherTypes[key];
}

//function weatherTypeText(weatherType) {
//    const key = weatherType === null ? 'both' : weatherType;
//    const weatherTypes = appContext.model.inputs.ruleEdit.weatherTypes;
//    return weatherTypes[key];
//}