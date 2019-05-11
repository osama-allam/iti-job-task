import React, { Component } from 'react';
import {Form, Button, Input, Select, Layout, Loading} from 'element-react/next';
import cssClasses from './edit.module.css';
import * as CompaniesDB from '../../DB/companiesDB'; 
import * as ProductssDB from '../../DB/productsDB'; 

class Edit extends Component {
  constructor(props) {
    super(props);
  
    this.state = {
      Loading: true,
      companies: null,
      product: null,
      form: {
        id: '',
        imageUrl: '',
        name: '',
        companyId: '',
        price: ''
      },
      rules: {
        imageUrl: [
          { required: true, message: 'Please input the Image URL', trigger: 'blur' },
          { validator: (rule, value, callback) => {
            if (value === '') {
              callback(new Error('Please input the Image URL'));
            } else {
              callback();
            }
          } }
        ],
        companyId: [
          { required: true,type: 'number', message: 'Please select A Company', trigger: 'change' }
        ],
        name: [
          { required: true, message: 'Please input the Name', trigger: 'blur' },
          { validator: (rule, value, callback) => {
            
            if (value === '') {
              callback(new Error('Please input the Name'));
            } else {
              callback();
            }
          } }
        ],
        price: [
          { required: true, type: 'number', message: 'Please input the Price', trigger: 'blur' },
          { validator: (rule, value, callback) => {
            var price = parseInt(value);
            setTimeout(() => {
              if (!Number.isInteger(price)) {
                callback(new Error('Please input digits'));
              } else{
                if (price < 1) {
                  callback(new Error('Price must be greater than 0'));
                } else {
                  callback();
                }
              }
            }, 1000);
          }, trigger: 'change' }
        ]
      }
    };
  }
  
  componentDidMount()
  {
    CompaniesDB.getAll()
    .then( res => {
      this.setState({
        companies: res.data
      });
    });
    ProductssDB.getById(this.props.match.params.id)
    .then( res => {
      this.setState({
        product: res.data,
        form: res.data,
        loading: false
      });
    });
  }

  handleSubmit(e) 
  {
    e.preventDefault();
    this.setState({loading: true});
    this.refs.form.validate((valid) => {
      if(valid) 
      {
        ProductssDB.update(this.state.form.id, this.state.form)
        .then( res => {
            if(res.status === 204) this.props.history.push("/products");
            else this.setState({loading: false});
          }
        )
        .catch( err => {
            console.error(err);
            this.setState({loading: false});
          }
        );
      }
      else return false;
    });
  }
  
  handleReset(e) {
    e.preventDefault();
  
    this.refs.form.resetFields();
    this.setState({
      form: this.state.product
    });
  }
  
  onChange(key, value) {
    if(key === "price" && !isNaN(parseInt(value))) value = parseInt(value);
  
    this.setState({
      form: Object.assign({}, this.state.form, { [key]: value })
    });
  }
  
  render() {
    let selectoptions = null;
    if(this.state.companies)
    {
      selectoptions = this.state.companies.map(c => 
        <Select.Option label={c.name} value={c.id} key={c.id} />
        ); 
    }
    return (
      <Loading loading={this.state.loading}>
       <Layout.Row gutter="20">
        <Layout.Col span="2" offset="1">
          <img className={cssClasses.formImage} src={this.state.form.imageUrl} alt="Product's"/>
        </Layout.Col>
        <Layout.Col span="20">
          <Form className={`${cssClasses.editForm} demo-ruleForm`} ref="form" model={this.state.form} rules={this.state.rules} labelWidth="100">
            <Form.Item label="Name" prop="name">
              <Input type="text" value={this.state.form.name} onChange={this.onChange.bind(this, 'name')} autoComplete="off" />
            </Form.Item>
            <Form.Item label="Image URL" prop="imageUrl">
              <Input type="text" value={this.state.form.imageUrl} onChange={this.onChange.bind(this, 'imageUrl')} autoComplete="off" />
            </Form.Item>
            <Form.Item label="Price" prop="price">
              <Input type="number" value={this.state.form.price} onChange={this.onChange.bind(this, 'price')}></Input>
            </Form.Item>
            <Form.Item label="Producer" prop="companyId">
            <Select value={this.state.form.companyId} placeholder="Please Select A Producer" onChange={this.onChange.bind(this, 'companyId')}>
              {selectoptions}
            </Select>
          </Form.Item>
            <Form.Item>
              <Button type="primary" onClick={this.handleSubmit.bind(this)}>Submit</Button>
              <Button onClick={this.handleReset.bind(this)}>Reset</Button>
            </Form.Item>
          </Form>
        </Layout.Col>
      </Layout.Row>
      </Loading>
    )
  }
}

export default Edit;
