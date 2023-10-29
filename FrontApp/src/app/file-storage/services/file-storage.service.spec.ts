import { TestBed } from '@angular/core/testing';

import { FileStorageService } from './file-storage.service';
import {HttpClient} from '@angular/common/http';

describe('FileStorageService: ', () => {
  let service: FileStorageService;
  const fakeHttpClient = { };

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        FileStorageService,
        { provide: HttpClient, useValue: fakeHttpClient }
      ]
    });
    service = TestBed.inject(FileStorageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('the method "correctFiles" must return true', () => {
    spyOn(service, 'acceptableExtensions').and.returnValue(['.yyy', '.png']);
    const fileList = createMockFileList([
      {
        body: 'test1',
        mimeType: 'text/plain',
        name: `file1.yyy`
      },
      {
        body: 'test2',
        mimeType: 'text/plain',
        name: `file2.png`
      }
    ]);

    const checked = service.correctFiles(fileList) ;
    expect(checked).toBe(true);
  });

  it('the method "checkFilesSize" must return false because files is null', () => {
    const checked = service.checkFilesSize(null);
    expect(checked).toBe(false);
  });
});


interface MockFile {
  name: string;
  body: string;
  mimeType: string;
}

const createFileFromMockFile = (file: MockFile): File => {
  const blob = new Blob([file.body], { type: file.mimeType }) as any;
  blob.lastModifiedDate = new Date();
  blob.name = file.name;
  return blob as File;
};

const createMockFileList = (files: MockFile[]) => {
  const fileList: FileList = {
    length: files.length,
    item(index: number): File {
      return fileList[index];
    }
  };
  files.forEach((file, index) => fileList[index] = createFileFromMockFile(file));

  return fileList;
};
