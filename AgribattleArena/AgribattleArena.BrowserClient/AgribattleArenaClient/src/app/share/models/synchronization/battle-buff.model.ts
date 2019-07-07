export interface IBattleBuff {
    /*public int Id { get; set; }
    public string NativeId { get; set; }
    public float Mod { get; set; }
    public float? Duration { get; set; }*/
    id: number;
    nativeId: string;
    mod: number;
    duration?: number;
}
