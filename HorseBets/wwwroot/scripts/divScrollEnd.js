export function checkDivScrollEnd(elementId, functionName, dotNetReference) {
    var div = document.getElementById(elementId);
    div.addEventListener('scroll', () => {
        var isScrolledToBottom = div.scrollHeight - div.clientHeight <= div.scrollTop + 1;
        if (isScrolledToBottom) {
            dotNetReference.invokeMethodAsync(functionName);
        }
    });
}