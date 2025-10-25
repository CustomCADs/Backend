import { axios, config } from '@/api/axios';
import * as allResources from './all';
import * as singleResources from './single';
import * as createResources from './create';
import * as editResources from './edit';
import * as deleteResources from './delete';
import { TagResponse } from '../common';

export const all = async () =>
	await axios.get<TagResponse[]>(allResources.url());

export const single = async (req: singleResources.Request) =>
	await axios.get<TagResponse>(singleResources.url(req));

export const create = async (req: createResources.Request) =>
	await axios.post<TagResponse>(
		createResources.url(),
		req,
		config({ idempotencyKey: req.idempotencyKey }),
	);

export const edit = async (req: editResources.Request) =>
	await axios.put(editResources.url(), req);

export const delete_ = async (req: deleteResources.Request) =>
	await axios.delete(deleteResources.url(), config({ data: req }));
