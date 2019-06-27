import { Route } from '@angular/compiler/src/core';
import { UrlSegmentGroup, UrlSegment } from '@angular/router';

export function mainMatcher(segments: UrlSegment[]) {

    return {consumed: segments};
}
