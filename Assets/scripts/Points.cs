sealed class Points{

    public int point;

    private Points() { }

    private static Points ex_point = new Points();

    public static Points GetInstance()
    {
        return ex_point;
    }
}
