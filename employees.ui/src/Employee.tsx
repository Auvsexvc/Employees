import React, { Component } from 'react';
import { variables } from './Variables.js';

export class Employee extends Component<any, any>{
    constructor(props) {
        super(props);

        this.state = {
            departments: [],
            employees: [],
            modalTitle: "",
            EmployeeName: "",
            EmployeeId: 0,
            Department: "",
            DateOfJoining: "",
            PhotoFileName: "anonymous.png",
            PhotoPath: variables.PHOTO_URL
        }
    }

    refreshList() {
        fetch(variables.API_URL + 'employee')
            .then(response => response.json())
            .then(data => {
                this.setState({ employees: data });
            });

        fetch(variables.API_URL + 'department')
            .then(response => response.json())
            .then(data => {
                this.setState({ departments: data });
            });
    }

    sortResult(prop, asc) {
        var sortedData = this.state.employees.sort(function (a, b) {
            if (asc) {
                return (a[prop] > b[prop]) ? 1 : ((a[prop] < b[prop]) ? -1 : 0)
            }
            else {
                return (b[prop] > a[prop]) ? 1 : ((b[prop] < a[prop]) ? -1 : 0)
            }
        });

        this.setState({ employees: sortedData });
    }

    componentDidMount() {
        this.refreshList();
    }

    changeEmployeeName = (e) => {
        this.setState({
            EmployeeName: e.target.value
        })
    }

    changeDepartment = (e) => {
        this.setState({
            Department: e.target.value
        })
    }
    changeDateOfJoining = (e) => {
        this.setState({
            DateOfJoining: e.target.value
        })
    }

    addClick() {
        this.setState({
            modalTitle: "Add Employee",
            EmployeeId: 0,
            EmployeeName: "",
            Department: "",
            DateOfJoining: "",
            PhotoFileName: "anonymous.png",
        })
    }

    editClick(emp) {


        this.setState({
            modalTitle: "Edit Employee",
            EmployeeId: emp.Id,
            EmployeeName: emp.Name,
            Department: emp.Department,
            DateOfJoining: (emp.DateOfJoining).toString().slice(0,10),
            PhotoFileName: emp.PhotoFileName,
        })
    }

    createClick() {
        fetch(variables.API_URL + 'employee', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Name: this.state.EmployeeName,
                Department: this.state.Department,
                DateOfJoining: this.state.DateOfJoining,
                PhotoFileName: this.state.PhotoFileName,
            })
        })
            .then(res => res.json())
            .then(result => this.refreshList())
    }

    updateClick(id) {
        fetch(variables.API_URL + 'employee/' + id, {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Id: this.state.EmployeeId,
                Name: this.state.EmployeeName,
                Department: this.state.Department,
                DateOfJoining: this.state.DateOfJoining,
                PhotoFileName: this.state.PhotoFileName,
            })
        })
            .then(res => res.json())
            .then(result => this.refreshList())
    }

    deleteClick(id) {
        if (window.confirm('Are you sure?')) {
            fetch(variables.API_URL + 'employee/' + id, {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                }
            })
                .then(res => res.json())
                .then(result => this.refreshList())
        }
    }

    imageUpload = (e) => {
        e.preventDefault();

        const formData = new FormData();
        formData.append("file", e.target.files[0], e.target.files[0].name);

        fetch(variables.API_URL + 'employee/savefile', {
            method: 'POST',
            body: formData
        })
            .then(res => res.json())
            .then(data => {
                this.setState({ PhotoFileName: data });
            })
    }

    render() {
        const {
            departments,
            employees,
            modalTitle,
            EmployeeName,
            EmployeeId,
            Department,
            DateOfJoining,
            PhotoFileName,
            PhotoPath,
        } = this.state;



        return (
            <div>
                <button type="button" className="btn btn-primary m2 float-end" data-bs-toggle="modal" data-bs-target="#exampleModal" onClick={() => this.addClick()}>Add Employee</button>
                <table className="table table-striped">
                    <thead>
                        <tr>
                            <th>
                                <div className="d-flex">
                                    <button type="button" className="btn btn-light" onClick={() => this.sortResult('Id', true)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-caret-down-fill" viewBox="0 0 16 16">
                                            <path d="M7.247 11.14 2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z" />
                                        </svg>
                                    </button>
                                    <button type="button" className="btn btn-light" onClick={() => this.sortResult('Id', false)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-caret-up-fill" viewBox="0 0 16 16">
                                            <path d="m7.247 4.86-4.796 5.481c-.566.647-.106 1.659.753 1.659h9.592a1 1 0 0 0 .753-1.659l-4.796-5.48a1 1 0 0 0-1.506 0z" />
                                        </svg>
                                    </button>

                                </div>
                                EmployeeId
                            </th>
                            <th>
                                <div className="d-flex">
                                    <button type="button" className="btn btn-light" onClick={() => this.sortResult('Name', true)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-caret-down-fill" viewBox="0 0 16 16">
                                            <path d="M7.247 11.14 2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z" />
                                        </svg>
                                    </button>
                                    <button type="button" className="btn btn-light" onClick={() => this.sortResult('Name', false)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-caret-up-fill" viewBox="0 0 16 16">
                                            <path d="m7.247 4.86-4.796 5.481c-.566.647-.106 1.659.753 1.659h9.592a1 1 0 0 0 .753-1.659l-4.796-5.48a1 1 0 0 0-1.506 0z" />
                                        </svg>
                                    </button>

                                </div>
                                EmployeeName
                            </th>
                            <th>
                                <div className="d-flex">
                                    <button type="button" className="btn btn-light" onClick={() => this.sortResult('Department', true)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-caret-down-fill" viewBox="0 0 16 16">
                                            <path d="M7.247 11.14 2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z" />
                                        </svg>
                                    </button>
                                    <button type="button" className="btn btn-light" onClick={() => this.sortResult('Department', false)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-caret-up-fill" viewBox="0 0 16 16">
                                            <path d="m7.247 4.86-4.796 5.481c-.566.647-.106 1.659.753 1.659h9.592a1 1 0 0 0 .753-1.659l-4.796-5.48a1 1 0 0 0-1.506 0z" />
                                        </svg>
                                    </button>

                                </div>
                                Department
                            </th>
                            <th>
                                <div className="d-flex">
                                    <button type="button" className="btn btn-light" onClick={() => this.sortResult('DateOfJoining', true)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-caret-down-fill" viewBox="0 0 16 16">
                                            <path d="M7.247 11.14 2.451 5.658C1.885 5.013 2.345 4 3.204 4h9.592a1 1 0 0 1 .753 1.659l-4.796 5.48a1 1 0 0 1-1.506 0z" />
                                        </svg>
                                    </button>
                                    <button type="button" className="btn btn-light" onClick={() => this.sortResult('DateOfJoining', false)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-caret-up-fill" viewBox="0 0 16 16">
                                            <path d="m7.247 4.86-4.796 5.481c-.566.647-.106 1.659.753 1.659h9.592a1 1 0 0 0 .753-1.659l-4.796-5.48a1 1 0 0 0-1.506 0z" />
                                        </svg>
                                    </button>

                                </div>
                                DateOfJoining
                            </th>
                            <th>
                                Options
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        {employees.map(emp =>
                            <tr key={emp.Id}>
                                <td>{emp.Id}</td>
                                <td>{emp.Name}</td>
                                <td>{emp.Department}</td>
                                <td>{new Date(emp.DateOfJoining).toLocaleDateString()}</td>
                                <td>
                                    <button type="button" className="btn btn-light -mr-1" data-bs-toggle="modal" data-bs-target="#exampleModal" onClick={() => this.editClick(emp)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                            <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z" />
                                            <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z" />
                                        </svg>
                                    </button>

                                    <button type="button" className="btn btn-light -mr-1" onClick={() => this.deleteClick(emp.Id)}>
                                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash-fill" viewBox="0 0 16 16">
                                            <path d="M2.5 1a1 1 0 0 0-1 1v1a1 1 0 0 0 1 1H3v9a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V4h.5a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H10a1 1 0 0 0-1-1H7a1 1 0 0 0-1 1H2.5zm3 4a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 .5-.5zM8 5a.5.5 0 0 1 .5.5v7a.5.5 0 0 1-1 0v-7A.5.5 0 0 1 8 5zm3 .5v7a.5.5 0 0 1-1 0v-7a.5.5 0 0 1 1 0z" />
                                        </svg>
                                    </button>
                                </td>
                            </tr>

                        )
                        }
                    </tbody>
                </table>
                <div className="modal fade" id="exampleModal" tabIndex={-1} aria-hidden="true">
                    <div className="modal-dialog modal-lg modal-dialog-centered">
                        <div className="modal-content">
                            <div className="modal-header">
                                <h5 className="modal-title">{modalTitle}</h5>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close">
                                </button>
                            </div>
                            <div className="modal-body">
                                <div className="d-flex flex-r bd-highlight mb-3">
                                    <div className="p-2 w-50 bd-highlight">
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Employee Name</span>
                                            <input type="text" className="form-control" value={EmployeeName} onChange={this.changeEmployeeName} />
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">Department</span>
                                            <select className="form-select" onChange={this.changeDepartment} value={Department}>
                                                {departments.map(dep => <option key={dep.Id}>
                                                    {dep.Name}
                                                </option>)}
                                            </select>
                                        </div>
                                        <div className="input-group mb-3">
                                            <span className="input-group-text">DateOfJoining</span>
                                            <input type="date" className="form-control" value={ DateOfJoining } onChange={this.changeDateOfJoining} />
                                        </div>
                                    </div>
                                    <div className="p-2 w-50 bd-highlight">
                                        <img width="250px" height="250px" alt="avatar" src={PhotoPath + '/' + PhotoFileName} />
                                        <input className= "m-2" type="file" onChange={this.imageUpload}/>
                                    </div>

                                </div>
                                {EmployeeId === 0 ? <button type="button" className="btn btn-primary float-start" onClick={() => this.createClick()} data-bs-dismiss="modal">Create</button> : null}

                                {EmployeeId !== 0 ? <button type="button" className="btn btn-primary float-start" onClick={() => this.updateClick(EmployeeId)} data-bs-dismiss="modal">Update</button> : null}
                                <button type="button" className="btn btn-secondary float-end" data-bs-dismiss="modal" aria-label="Close">Back</button>

                            </div>
                        </div>
                    </div>
                </div>

            </div>
        )
    }
}