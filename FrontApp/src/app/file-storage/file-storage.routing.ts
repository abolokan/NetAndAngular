import {RouterModule, Routes} from '@angular/router';

// components
import {FileListComponent} from './components';

const routes: Routes = [
   { path: '', component: FileListComponent }
];

export const FileStorageRouting = RouterModule.forChild(routes);

