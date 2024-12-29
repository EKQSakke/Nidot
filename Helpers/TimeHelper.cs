namespace Nidot;

public static class TimeHelper {
    public static string SecondsToFormattedString(double delta) {
        var minutes = (int)(delta / 60);
		var seconds = delta - minutes * 60;
		var zero = seconds < 10 ? "0" : "";
		return $"{minutes}:{zero}{seconds:N1}";
    }

}