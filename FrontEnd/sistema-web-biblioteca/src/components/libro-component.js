import React, { Component } from "react";
import libroService from "../services/libro-service";

export default class Libro extends Component {

  constructor(props) {
    super(props);
    this.getLibros = this.getLibros.bind(this);
    this.refreshLibros = this.refreshLibros.bind(this);
    this.onChangeAutor = this.onChangeAutor.bind(this);
    this.onChangeTitulo = this.onChangeTitulo.bind(this);
    this.addNew = this.addNew.bind(this);
    this.newLibros = this.newLibros.bind(this);
    this.deleteLibros = this.deleteLibros.bind(this);
    this.getLibro = this.getLibro.bind(this);
    this.prestarLibro=this.prestarLibro.bind(this);
    this.state = {
        libros: [],
        id: "",
        titulo: "",
        autor: "", 
        disponibleParaPrestamo: true,
        submitted: false
      };
  }

  componentDidMount() {
    this.getLibros();
  }

  getLibros() {
    libroService.getAllLibros()
      .then(response => {
        this.setState({
          libros: response.data
        });
        console.log(response.data);
      })
      .catch(e => {
        console.log(e);
      });
  }

  refreshLibros() {
    this.getLibros();
  }

  onChangeTitulo(e) {
    this.setState({
      titulo: e.target.value
    });
  }

  onChangeAutor(e) {
    this.setState({
      autor: e.target.value
    });
  }

  addNew() {
    var data = {
      titulo: this.state.titulo,
      autor: this.state.autor,
      disponibleParaPrestamo:true
    };

  //  if (this.state.isUpdate === false){
      libroService.addNewLibros(data)
      .then(response => {
        this.setState({
          id: response,
          submitted: true
        });
        console.log(response.data);
        this.refreshLibros();
      })
      .catch(e => {
        console.log(e);
      });
 //   } 
    // else {
    //   libroService.update(data)
    //   .then(response => {
    //     this.setState({
    //       id: response.data.id,
    //       name: response.data.name,
    //       price: response.data.price,
    //       isDeleted: response.data.isDeleted,
    //       submitted: true, 
    //       isUpdate: true
    //     });
    //     console.log(response.data);
    //     this.refreshProducts();
    //   })
    //   .catch(e => {
    //     console.log(e);
    //   });
    // }
  }

  newLibros() {
    this.setState({
      id: "",
      titulo: "",
      autor: "", 
      disponibleParaPrestamo: true,
      submitted: false
    });
    this.refreshLibros();
  }

  deleteLibros(id){
    var data = {
      id: id
    };
    console.log("Data libros : ", data);

    libroService.deleteLibro(data)
      .then(response => {
        // this.setState({
        //   id: response.data.id,
        //   name: response.data.name,
        //   price: response.data.price,
        //   isDeleted: true
        // });
        this.refreshLibros();
      })
      .catch(exception => {
        console.log(exception);
      });
  }

  getLibro(id){
    var data = {
      id: id
    };
    console.log("Data libros : ", data);

    libroService.getOneLibro(data)
      .then(response => {
        this.setState({
          id: response.data.id,
          titulo: response.data.titulo,
          autor: response.data.autor,
          disponibleParaPrestamo:response.data.disponibleParaPrestamo
        });
        this.refreshLibros();
      })
      .catch(exception => {
        console.log(exception);
      });
  }
  prestarLibro(data)
  {
    libroService.prestarLibro(data)
    .then(response => {
      this.setState({
        id: response.data.id,
        titulo: response.data.titulo,
        autor: response.data.autor,
        disponibleParaPrestamo:response.data.disponibleParaPrestamo
      });
      this.refreshLibros();
    })
    .catch(exception => {
      console.log(exception);
    });
  }

  devolverLibro(data)
  {
    libroService.devolverLibro(data)
    .then(response => {
      this.setState({
        id: response.data.id,
        titulo: response.data.titulo,
        autor: response.data.autor,
        disponibleParaPrestamo:response.data.disponibleParaPrestamo
      });
      this.refreshLibros();
    })
    .catch(exception => {
      console.log(exception);
    });
  }
  render() {
    const { libros } = this.state;
    return (
      <><div className="submit-form">
        {this.state.submitted ? (
          <div>
            <h4>Successfully submitted!</h4>
            <button className="btn btn-success" onClick={this.newLibros}>Add New One</button>
          </div>
        ) : (
          <div>
            <div className="form-group">
              <label htmlFor="name">Titulo</label>
              <input type="text" className="form-control" id="titulo" required value={this.state.titulo} onChange={this.onChangeTitulo} name="name" />
            </div>

            <div className="form-group">
              <label htmlFor="price">Autor</label>
              <input type="text" className="form-control" id="autor" required value={this.state.autor} onChange={this.onChangeAutor}name="price" />
            </div>
           
            <br></br>
            <button onClick={this.addNew} className="btn btn-success">
              Submit
            </button>
          </div>
        )}
      </div>
      <br></br>
      <br></br>
      <div className="list row">
          <div className="col-md-6">
            <h4> Libros habilitados</h4>
            <br></br>
            <table className="table table-striped table-bordered">
              <thead>
                <tr>
                  <th> Id</th>
                  <th>Titulo</th>
                  <th>Autor</th>
                  <th>DisponibleParaPrestamo</th>
                </tr>
              </thead>
              <tbody>
                {libros && libros.map(libro => <tr key={libro.id}>
                  <td>{libro.id}</td>
                  <td>{libro.titulo}</td>
                  <td>{libro.autor}</td>
                  <td>{libro.disponibleParaPrestamo?"Si":"No"}</td>
                  <td><button className="btn btn-success" onClick={() => libro.disponibleParaPrestamo?this.prestarLibro(libro):this.devolverLibro(libro)}>{libro.disponibleParaPrestamo?"Prestar libro":"Devolver libro"}</button></td>
                  <td><button className="btn btn-danger" onClick={() => this.deleteLibros(libro.id)}>Delete</button></td>
                </tr>
                )}
              </tbody>
            </table>
          </div>
        </div></>
    );
  }
}
