class Score {
    private static double score;

    public static void increment(int add) {
        score += add;
    }

    public static double getScore() {
        return score;
    }
}