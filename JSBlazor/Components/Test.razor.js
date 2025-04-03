
export function showMessage(text) {
    alert(text);
}

export function runSample(time, text, dotnet) {
    console.log('Start: runSample');
    setTimeout(() => {
        console.log('Executed: runSample');
        showMessage(text);
        dotnet.invokeMethodAsync('SetValue', text);
    }, time);
}