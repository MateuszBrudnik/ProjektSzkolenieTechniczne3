import { Component } from '@angular/core';
import { ApiService } from '../services/api.service';

@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.css']
})
export class UploadComponent {
  fileToUpload: File | null = null;
  uploadedFileUrl: string | null = null;

  constructor(private apiService: ApiService) { }

  handleFileInput(event: Event) {
    const target = event.target as HTMLInputElement;
    if (target.files && target.files.length > 0) {
      this.fileToUpload = target.files[0];
    }
  }

  uploadFileToActivity() {
    if (this.fileToUpload) {
      this.apiService.uploadRepairFile(1, this.fileToUpload).subscribe((response: any) => {
        this.uploadedFileUrl = response.Url;
      });
    }
  }
}
