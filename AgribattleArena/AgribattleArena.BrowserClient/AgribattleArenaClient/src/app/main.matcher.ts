import { Route } from '@angular/compiler/src/core';
import { UrlSegmentGroup, UrlSegment } from '@angular/router';
import { ProfileService } from './share/profile.service';

export function mainMatcher(segments: UrlSegment[]) {

    return {consumed: segments};
}
