import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AgendaServiceService } from '../services/agenda-service.service';

@Component({
  selector: 'meeting-info',
  templateUrl: './meeting-info.component.html',
  styleUrls: ['./meeting-info.component.css']
})
export class MeetingInfoComponent implements OnInit {
  agendaType = []

  agendaTypeRoleMatrix:any = []

  approverRoles:any;

  filteredApproverRoles:any;

  approvers:any = [];

  filteredApprovers:any;

  constructor(
    private fb: FormBuilder,private agendaService: AgendaServiceService,
    private toastr: ToastrService) { }

  agendaForm = this.fb.group({
    agendaId: [],
    agendaItem1: ['', Validators.required],
    agendaTypeId: ['', Validators.required],
    approverId: ['', Validators.required],
    roleId: ['', Validators.required]
  });

  ngOnInit(): void {
    this.initData()
  }

  initData(){

    this.agendaService.getAgendaTypes().subscribe(res =>{
      this.agendaType = res;
    });

    this.agendaService.getAgendaMatrix().subscribe(res =>{
      this.agendaTypeRoleMatrix = res;
    });

    this.agendaService.getUserRoles().subscribe(res=>{
      this.approverRoles = res
    })

    this.agendaService.getApprovers().subscribe(res =>{
      this.approvers = res
    })
  }

  changeAgendaType($event: any){
    
    this.filteredApproverRoles = []
    this.agendaForm.patchValue({roleId: ''});

    console.log($event)
    var roles = this.agendaTypeRoleMatrix.filter((x:{ agendaType: any; }) => x.agendaType == $event['agendaTypeId']).map((obj: { roleId: any; }) => obj.roleId)
    console.log(roles)
    this.filteredApproverRoles = this.approverRoles.filter((f: { roleId: number; }) => roles.includes(f.roleId))
  }

  changeRole($event: any){
    this.filteredApprovers = []
    this.agendaForm.patchValue({approverId: ''});

    this.filteredApprovers = this.approvers.filter((x: { roleId: any; }) => x.roleId == $event['roleId'])
  }

  submitAgenda(){
    var data = this.agendaForm.value;

    if(data.agendaItem1 != undefined && data.agendaItem1.length > 10){

      this.toastr.error("Item Description More that 512 charcters")

      return;
    }

    console.log(data)
    if(data.agendaId != undefined){
      this.agendaService.updateAgendaItem(data.agendaId, data).subscribe(res =>{
        this.agendaForm.patchValue(res)
      })
    }
    else{
      delete data.agendaId;
      this.agendaService.createAgendaItem(data).subscribe(res =>{
        this.agendaForm.patchValue(res)

        console.log(this.agendaForm.value)
      })
    }
  }

}
