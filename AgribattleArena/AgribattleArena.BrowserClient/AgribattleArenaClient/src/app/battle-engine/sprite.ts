import { INativesStoreMapped } from '../share/models/natives-store-mapped.model';
import { ISpriteNative } from '../share/models/natives/mapped';

export class Sprite {

    native: ISpriteNative;

    constructor(id: string, natives: INativesStoreMapped) {
        this.native = natives.sprites.find(x => x.id === id);
    }

    update(milliseconds: number) {

    }
}
